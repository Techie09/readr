using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore;
using Readr.Api;

namespace APIServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create an instance of a Web Host using WEbHostBuilder
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// takes a set of args when application starts and configures
        /// Web Hosting for the application.
        /// CreateDefaultBuilder sets default configurations commonly
        /// used for applications including:
        /// * use kestrel as the web server
        /// * configure using the applications configuration proviers
        /// * set the Microsoft.Extensions.Hosting.IHostEnvironment.ContentRootPath
        ///   to the result of <code>GetCurrentDirectory()</code>
        /// * load <see cref="IConfiguration"/> from appsettings.json and
        ///   appsettings.[Microsoft.Extensions.Hosting.IHostEnvironment.EnvironmentName].json
        /// * load IConfiguration from User Secrets when Microsoft.Extensions.Hosting.IHostEnvironment.EnvironmentName is 'Development'
        /// * load IConfiguration from environment variables
        /// * configure the Microsoft.Extensions.Logging.ILoggerFactory to log the console and debug output
        /// * adds the Hostfiltering middleware
        /// * adds the ForwardedHeaders middleware if ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
        /// * Enable IIS Integration.
        /// 
        /// This will use <see cref="Startup"/> to further configure Logging, Dependcy injection and other startup tasks
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
