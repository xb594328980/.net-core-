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
using Sansunt.MicroService.Demo.Extensions;

namespace Sansunt.MicroService.Demo.OrderManage
{
    public class Startup
    {
        private readonly ConsulService _consulService;
        private readonly HealthService _healthService;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _consulService = Configuration.GetSection("Consul").Get<ConsulService>();
            _healthService = Configuration.GetSection("Service").Get<HealthService>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.RegisterConsul(lifetime, _healthService, _consulService);
            app.RegisterConsul(options =>
            {
                options.ConsulServiceConfig=new ConsulService(){Ip = "123456" };
            });
        }
    }
}
