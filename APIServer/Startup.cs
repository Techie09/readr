using System.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Readr.Api.InputFormatters;
using Readr.Models;
using Readr.Repositories;
using Readr.Repositories.Interfaces;
using Readr.Services;
using Readr.Services.Interfaces;

namespace Readr.Api
{
    public class Startup
    {
#pragma warning disable CS0618 // IHostingEnvironment is obsolete; use IHostEnvironment instead
        private readonly IHostingEnvironment _env;
        private IConfiguration _config;
        private readonly ILoggerFactory _loggerFactory;

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
#pragma warning restore CS0618 // IHostingEnvironment is obsolete; use IHostEnvironment instead

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoDbSettings>(sp => _config.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>())
                .AddSingleton<IAzureSettings>(ServiceProvider => _config.GetSection(nameof(AzureSettings)).Get<AzureSettings>());

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddMvcOptions(o => o.InputFormatters.Insert(0, new RawRequestBodyFormatter()));

            //services.AddControllers()

            //handle dependency injections
            services.AddScoped<IAppUserService, AppUserService>()
                .AddScoped<IAppUserRepository, AppUserRepository>()
                .AddScoped<IUserSessionService, UserSessionService>()
                .AddScoped<IUserSessionRepository, UserSessionRepository>()
                .AddScoped<ISessionDetailRepository, SessionDetailRepository>()
                .AddScoped<IAzureService, AzureService>();
        }

        public void Configure(IApplicationBuilder app)
        {
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
            }

            //Use for MVC 
            app.UseMvc();
        }
    }
}
