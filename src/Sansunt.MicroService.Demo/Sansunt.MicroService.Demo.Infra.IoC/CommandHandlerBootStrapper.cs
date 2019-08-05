using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using MediatR;
using Sansunt.Domain.Core.Bus;
using Sansunt.Domain.Core.Events;
using Sansunt.Domain.Core.Notifications;
using Sansunt.Infra.Tools.Dependency;
using Module = Autofac.Module;
using System.Runtime.InteropServices;
using Sansunt.MicroService.Demo.Domain.CommandHandlers;
using Sansunt.MicroService.Demo.Domain.Notifications;

namespace Sansunt.MicroService.Demo.Infra.Ioc
{
    /// <summary>
    /// 命令注册
    /// <remarks>
    /// create by xingbo 19/05/15
    /// </remarks>
    /// </summary>
    public class CommandHandlerBootStrapper : Module, IConfig
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(StaffCommandHandler).GetTypeInfo().Assembly).Where(x => x.FullName.Contains("CommandHandler"))
                .AsImplementedInterfaces().PropertiesAutowired();

            // 将事件模型和事件处理程序匹配注入
            builder.RegisterType<DomainNotificationHandler>().As<INotificationHandler<DomainNotification>>().InstancePerLifetimeScope();


        }
    }
}
