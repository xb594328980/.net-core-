using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sansunt.MicroService.Demo.Infra.Mapper
{
    /// <summary>
    /// AutoMapper 的启动服务
    /// </summary>
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            //启动配置
            //var config = AutoMapperConfig.RegisterMappings();
            //services.AddSingleton<IMapper>(sp => AutoMapperConfig.InstanceMapping());
            var mapper = AutoMapperConfig.StaticMapping();
            //mapper.ConfigurationProvider.AssertConfigurationIsValid();
            services.AddSingleton<IMapper>(mapper);//手动添加映射关系 
        }
    }

}
