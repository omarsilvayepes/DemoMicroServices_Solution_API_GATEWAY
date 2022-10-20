using Microsoft.EntityFrameworkCore;

namespace CustomerWebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this.Configuration=configuration;
        }

   
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            /* Database Context Dependency Injection */

            //Without Docker compose use the follow:

            //services.AddDbContext<CustomerDbContext>(options=> options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            //With Docker compose use the follow:

            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
            var connectionString = $"Server={dbHost}; Database={dbName}; User Id=sa; Password={dbPassword}";
            services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(connectionString));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
