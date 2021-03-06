﻿using System.Threading.Tasks;
using Fabric.FHIR.Configuration;
using Fabric.Platform.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy.Owin;
using Serilog.Core;

namespace Fabric.FHIR
{
    public class Startup
    {

        private readonly IConfiguration _config;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(env.ContentRootPath);

            _config = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWebEncoders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var appConfig = new AppConfiguration();
            ConfigurationBinder.Bind(_config, appConfig);
            var idServerSettings = appConfig.IdentityServerConfidentialClientSettings;

            var levelSwitch = new LoggingLevelSwitch();
            var logger = LogFactory.CreateLogger(levelSwitch, appConfig.ElasticSearchSettings, idServerSettings.ClientId);

            //Uncomment this when you want to protect the API using Fabric.Identity
            //app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            //{
            //    Authority = idServerSettings.Authority,
            //    RequireHttpsMetadata = false,

            //    ApiName = idServerSettings.ClientId
            //});

            app.UseOwin()
                .UseFabricLoggingAndMonitoring(logger, () => Task.FromResult(true), levelSwitch)
                // Uncomment this when you want to protect the API by restricting the scopes allowed
                //.UseAuthPlatform(idServerSettings.Scopes)
                .UseNancy(opt => opt.Bootstrapper = new Bootstrapper(logger, appConfig));
        }
    }
}
