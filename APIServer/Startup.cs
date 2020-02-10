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
        private readonly IHostingEnvironment _env;
        private IConfiguration _config;
        private readonly ILoggerFactory _loggerFactory;

        private string _connection = null;

        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json",
                             optional: false,
                             reloadOnChange: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            _loggerFactory = loggerFactory;
            _env = env;
            _config = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddRazorPages();

            var builder = new SqlConnectionStringBuilder(
            _config["connectionString"]);
            builder.Password = _config["DbPassword"];
            _connection = builder.ConnectionString;

            //add DbContext
            services.AddMvc();
            services.AddDbContext<AppUserContext>(options =>
            options.UseSqlServer(_connection));

            //handle dependency injections
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {
            //_env = env;
            //_loggerFactory = loggerFactory;
            var logger = _loggerFactory.CreateLogger<Startup>();

            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Development service configuration
                logger.LogInformation("Development environment");
            }
            else
            {
                app.UseExceptionHandler("/Error");

                // Non-development service configuration
                logger.LogInformation($"Environment: {_env.EnvironmentName}");
                
                //not sure what HSTS is at this time. may be only useful for production environments
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseRouting();
            //app.UseAuthorization();


            //Use for MVC 
            app.UseMvc();


            //Use for Razor Pages
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //});
        }
    }
}
