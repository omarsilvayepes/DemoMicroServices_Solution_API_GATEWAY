using JwtAuthenticationManger.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationManger
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "";
        private const int JWT_TOKEN_VALIDITY_MIN = 20;
        private readonly List<UserAccount> _userAccounts;

        public JwtTokenHandler()
        {
            _userAccounts = new List<UserAccount>
            {
                new UserAccount{Name="admin",Password="admin",Role="Administrator"},
                new UserAccount{Name="user01",Password="user01",Role="User"}
            };
        }


        public  AuthenticationResponse? GenerateJwtToken(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrWhiteSpace(authenticationRequest.UserName) || string.IsNullOrWhiteSpace(authenticationRequest.Password)) return null;
            /* Validation */
            var userAccount=_userAccounts.Where(opt => opt.Name.Equals(authenticationRequest.UserName) && opt.Password.Equals(authenticationRequest.Password)).FirstOrDefault();
            if(userAccount==null) return null;

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MIN);
            var tokenKey=Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name,authenticationRequest.UserName),
                new Claim(ClaimTypes.Role,userAccount.Role)
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler=new JwtSecurityTokenHandler();
            var securityToken=jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                UserName = userAccount.Name,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token
            };
        }
    }
}
