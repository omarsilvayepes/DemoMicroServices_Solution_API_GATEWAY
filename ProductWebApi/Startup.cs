using Microsoft.EntityFrameworkCore;

namespace ProductWebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            /* Database Context Dependency Injection */

            //Without Docker compose use the follow:

            //services.AddDbContext<ProductDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("DevConnection")));

            //With Docker compose use the follow:

            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD");
            var connectionString = $"server={dbHost};port=3306;database={dbName};user=root;password={dbPassword}";
            services.AddDbContext<ProductDbContext>(options => options.UseMySQL(connectionString));

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
