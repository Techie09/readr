using System.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Readr.Repositories;
using Readr.Repositories.Contexts;
using Readr.Repositories.Interfaces;
using Services;

namespace Readr.Api
{
    public class Startup
    {
        private readonly IHostEnvironment _env;
        private readonly IConfiguration _config;
        private readonly ILoggerFactory _loggerFactory;

        private string _connection = null;

        public Startup(IHostEnvironment env, IConfiguration config,
      ILoggerFactory loggerFactory)
        {
            _config = config;
            _loggerFactory = loggerFactory;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var logger = _loggerFactory.CreateLogger<Startup>();
            //services.AddRazorPages();

            var builder = new SqlConnectionStringBuilder(
            _config.GetConnectionString("connectionstring"));
            builder.Password = _config["DbPassword"];
            _connection = builder.ConnectionString;

            //add DbContext
            services.AddDbContext<AppUserContext>(options =>
            options.UseSqlServer(_connection));

            //handle dependency injections
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();

            if (_env.IsDevelopment())
            {
                // Development service configuration
                logger.LogInformation("Development environment");
            }
            else
            {
                // Non-development service configuration
                logger.LogInformation($"Environment: {_env.EnvironmentName}");
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                //not sure what HSTS is at this time. may be only useful for production environments
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseRouting();
            //app.UseAuthorization();
            
            //Use for MVC 
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            //Use for Razor Pages
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //});
        }
    }
}
