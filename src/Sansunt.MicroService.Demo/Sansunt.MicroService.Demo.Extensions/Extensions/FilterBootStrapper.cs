using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Sansunt.Infra.Tools.Dependency;
using Sansunt.MicroService.Demo.Extensions.Filters;

namespace Sansunt.MicroService.Demo.Extensions.Extensions
{
    /// <summary>
    /// web Filter注册器
    /// create by xingbo 
    /// </summary>
    public class FilterBootStrapper : Module, IConfig
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthAttribute>().AsSelf();
            builder.RegisterType<ErrorAttribute>().AsSelf();
            builder.RegisterType<RequestLogAttribute>().AsSelf();
            
        }
    }
}
