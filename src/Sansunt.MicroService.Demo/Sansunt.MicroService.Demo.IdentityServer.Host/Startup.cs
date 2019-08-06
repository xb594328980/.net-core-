using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using EasyCaching.Core.Configurations;
using EasyCaching.InMemory;
using EasyCaching.Redis;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sansunt.Infra.Caches.EasyCaching;
using Sansunt.Infra.Logs.Extensions;
using Sansunt.Infra.Tools;
using Sansunt.Infra.Tools.Helpers;
using Sansunt.Infra.Tools.Schedulers;
using Sansunt.MicroService.Demo.Extensions.Consul;
using Sansunt.MicroService.Demo.Extensions.Extensions;
using Sansunt.MicroService.Demo.Extensions.Filters;
using Sansunt.MicroService.Demo.IdentityServer.Host.Extensions;
using Sansunt.MicroService.Demo.Infra.Config;
using Sansunt.MicroService.Demo.Infra.Ioc;
using Sansunt.MicroService.Demo.Infra.Mapper;
using Swashbuckle.AspNetCore.Swagger;

namespace Sansunt.MicroService.Demo.IdentityServer.Host
{
    /// <summary>
    /// 启动配置
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly ConsulService _consulService;
        private readonly HealthService _healthService;
        //swagger配置
        private readonly SwaggerConfig swaggerConfig;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            swaggerConfig = Configuration.GetSection("SwaggerConfig").Get<SwaggerConfig>();
            _consulService = Configuration.GetSection("Consul").Get<ConsulService>();
            _healthService = Configuration.GetSection("Service").Get<HealthService>();
        }
        /// <summary>
        ///This method gets called by the runtime.Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region 添加Swagger
            if (swaggerConfig.Enable)
            {
                services.AddSwaggerGen(options =>
                {
                    //swagger中控制请求的时候发是否需要在url中增加accesstoken
                    options.OperationFilter<AddAuthTokenHeaderParameter>();
                    options.SwaggerDoc("v1", new Info { Title = "Sansunt.MicroService.Demo.IdentityServer.Host", Version = "v1" });
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Sansunt.MicroService.Demo.IdentityServer.Host.xml"));
                });
            }
            #endregion
            #region 添加mvc
            services.AddMvc(opt =>
            {
                var centralRoutePrefix = Configuration.GetSection("AppConfig:CentralRoutePrefix")?.Value;
                centralRoutePrefix = string.IsNullOrWhiteSpace(centralRoutePrefix) ? "api" : centralRoutePrefix;
                opt.UseCentralRoutePrefix(new RouteAttribute(centralRoutePrefix));

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            #endregion
            #region 跨域
            var urls = Configuration["AppConfig:Cores"].Split(',');
            services.AddCors(options =>
                options.AddPolicy("AllowSameDomain",
                    builder => builder.WithOrigins(urls).AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials())
            );
            #endregion 跨域
            #region AutoMapper
            services.AddAutoMapperSetup();
            #endregion
            #region NLog
            services.AddNLog();
            #endregion
            #region MediatR
            services.AddMediatR(typeof(Startup));
            #endregion
            #region 缓存
            services.AddCache(options =>
            {
                //options.UseInMemory();
                //use redis cache
                options.UseRedis(config =>
                {
                    config.DBConfig.Endpoints.Add(new ServerEndPoint("192.168.31.220", 6379));
                    config.DBConfig.Password = "123";
                }, "redis2");
            });
            #endregion
            #region IdentityServer4
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

            services.AddIdentityServer()
                .AddSigningCredential(new X509Certificate2("./certificate/cas.clientservice.pfx", "12345678"))
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients());
            #endregion
            #region autofac
            //services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
            //注册平台组件
            IServiceProvider serviceProvider = services.AddInfrastructure(
                new NativeInjectorBootStrapper(),
                new CommandHandlerBootStrapper(),
                new EventBootStrapper(),
                new FilterBootStrapper());//注册依赖
            #endregion
            return serviceProvider;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="lifetime"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseStaticFiles();//启用静态文件访问
            app.UseIdentityServer();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseAuthentication();//启用授权
            app.RegisterConsul(lifetime, _healthService, _consulService);
            #region Swagger
            if (swaggerConfig.Enable)
            {
                app.UseSwagger(c =>
                {
                    //设置json路径
                    c.RouteTemplate = "docs/{documentName}/swagger.json";
                });
                // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
                app.UseSwaggerUI(c =>
                {
                    //访问swagger UI的路由，如http://localhost:端口/docs
                    c.RoutePrefix = swaggerConfig.Path.Replace("\\", "/").TrimEnd('/').TrimStart('/');
                    c.SwaggerEndpoint("/docs/v1/swagger.json", "Sansunt.MicroService.Demo.IdentityServer.Host 接口文档");
                    //更改UI样式
                    //c.InjectStylesheet("/swagger-ui/custom.css");
                    //引入UI变更js
                    //c.InjectOnCompleteJavaScript("/swagger-ui/custom.js");
                });
            }
            #endregion Swagger
        }

    }
}
