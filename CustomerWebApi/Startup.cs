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

            services.AddDbContext<CustomerDbContext>(options=> options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
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
