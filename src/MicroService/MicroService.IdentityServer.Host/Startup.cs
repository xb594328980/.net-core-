using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using MicroService.IdentityServer.Host.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MicroService.IdentityServer.Host
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<Extensions.AppContext>();//注入DbContext

            services.AddTransient<IUserService, UserService>();//service注入
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddIdentityServer()
                 .AddSigningCredential(new X509Certificate2("./certificate/cas.clientservice.pfx", "12345678"))
                 .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResourceResources())
                 .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                 .AddInMemoryClients(IdentityServerConfig.GetClients())
                 ;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentityServer();
            app.UseMvc();
            app.RegisterConsul(lifetime, _healthService, _consulService);
        }
    }
}
