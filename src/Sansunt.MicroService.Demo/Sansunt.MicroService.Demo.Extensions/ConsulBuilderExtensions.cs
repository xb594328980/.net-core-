using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ocelot.Configuration.Repository;
using Ocelot.DownstreamRouteFinder.Finder;
using Ocelot.Middleware;
using Ocelot.Middleware.Pipeline;

namespace Sansunt.MicroService.Demo.Extensions
{
    public static class ConsulBuilderExtensions
    {

        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app,
            Action<RegisterConsulOptions> options)
        {
            RegisterConsulOptions defaultOption = new RegisterConsulOptions();
            options(defaultOption);
            return app;
        }

        // 服务注册
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IApplicationLifetime lifetime, HealthService healthService, ConsulService consulService)
        {
            var consulClient = new ConsulClient(x => x.Address = new Uri($"http://{consulService.Ip}:{consulService.Port}"));//请求注册的 Consul 地址
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(10),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                HTTP = $"http://{healthService.Ip}:{healthService.Port}/api/health",//健康检查地址
                Timeout = TimeSpan.FromSeconds(5)
            };
            // Register service with consul
            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = healthService.Name + "_" + healthService.Port,
                Name = healthService.Name,
                Address = healthService.Ip,
                Port = healthService.Port,
                Tags = new[] { $"urlprefix-/{healthService.Name}" }//添加 urlprefix-/servicename 格式的 tag 标签，以便Fabio识别

            };
            consulClient.Agent.ServiceRegister(registration).Wait();//服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
            });
            return app;
        }

        public static IApplicationBuilder UseOcelotWhenRouteMatch(this IApplicationBuilder app,
          Action<IOcelotPipelineBuilder, OcelotPipelineConfiguration> builderAction)
          => UseOcelotWhenRouteMatch(app, builderAction, new OcelotPipelineConfiguration());

        public static IApplicationBuilder UseOcelotWhenRouteMatch(this IApplicationBuilder app,
            Action<OcelotPipelineConfiguration> pipelineConfigurationAction,
            Action<IOcelotPipelineBuilder, OcelotPipelineConfiguration> builderAction)
        {
            var pipelineConfiguration = new OcelotPipelineConfiguration();
            pipelineConfigurationAction?.Invoke(pipelineConfiguration);
            return UseOcelotWhenRouteMatch(app, builderAction, pipelineConfiguration);
        }

        public static IApplicationBuilder UseOcelotWhenRouteMatch(this IApplicationBuilder app, Action<IOcelotPipelineBuilder, OcelotPipelineConfiguration> builderAction, OcelotPipelineConfiguration configuration)
        {
            app.MapWhen(context =>
            {
                // 获取 OcelotConfiguration
                var internalConfigurationResponse =
                    context.RequestServices.GetRequiredService<IInternalConfigurationRepository>().Get();
                if (internalConfigurationResponse.IsError || internalConfigurationResponse.Data.ReRoutes.Count == 0)
                {
                    // 如果没有配置路由信息，不符合分支路由的条件，直接退出
                    return false;
                }

                var internalConfiguration = internalConfigurationResponse.Data;
                var downstreamRouteFinder = context.RequestServices
                    .GetRequiredService<IDownstreamRouteProviderFactory>()
                    .Get(internalConfiguration);
                // 根据请求以及上面获取的Ocelot配置获取下游路由
                var response = downstreamRouteFinder.Get(context.Request.Path, context.Request.QueryString.ToString(),
                    context.Request.Method, internalConfiguration, context.Request.Host.ToString());
                // 如果有匹配路由则满足该分支路由的条件，交给 Ocelot 处理
                return !response.IsError
                       && !string.IsNullOrEmpty(response.Data?.ReRoute?.DownstreamReRoute?.FirstOrDefault()
                           ?.DownstreamScheme);
            }, appBuilder => appBuilder.UseOcelot(builderAction, configuration).Wait());

            return app;
        }
    }
}
