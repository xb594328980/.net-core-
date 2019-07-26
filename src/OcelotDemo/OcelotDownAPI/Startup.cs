using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OcelotDownAPI.Ext;

namespace OcelotDownAPI
{
    public class Startup
    {
        ConsulService _consulService = new ConsulService();
        HealthService _healthService = new HealthService();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Configuration.GetSection("Consul").Bind(_consulService);
            Configuration.GetSection("Service").Bind(_healthService);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //IdentityServerConfig identityServerConfig = new IdentityServerConfig();
            //Configuration.Bind("IdentityServerConfig", identityServerConfig);
            //services.AddAuthentication(identityServerConfig.IdentityScheme)
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.RequireHttpsMetadata = false;
            //        options.Authority = $"http://{identityServerConfig.IP}:{identityServerConfig.Port}";
            //        options.ApiName = identityServerConfig.ResourceName;
            //    }
            //    );



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseAuthentication();

            app.UseMvc();
            app.RegisterConsul(lifetime, _healthService, _consulService);
        }
    }
}
