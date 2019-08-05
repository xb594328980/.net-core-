using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Autofac.Core;
using MediatR;
using Sansunt.Domain.Core.Notifications;
using Sansunt.Domain.Core.Events;

using Sansunt.Domain.Core.Bus;

using Sansunt.Infra.Tools.Dependency;
using Module = Autofac.Module;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Sansunt.Infra.Tools.Dependency;
using Sansunt.MicroService.Demo.Domain.Interfaces;
using Sansunt.MicroService.Demo.Infra.Data.UoW;
using Sansunt.Infra.Bus;
using Sansunt.MicroService.Demo.Application.Services;
using Sansunt.MicroService.Demo.Infra.Data.Context;
using Sansunt.MicroService.Demo.Infra.Data.Repository;

namespace Sansunt.MicroService.Demo.Infra.Ioc
{
    /// <summary>
    /// 依赖关系绑定
    /// <remarks>create by xingbo 18/12/17</remarks>
    /// </summary>
    public class NativeInjectorBootStrapper : Module, IConfig
    {
        protected override void Load(ContainerBuilder builder)
        {


            #region mediator
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            #endregion
            #region Application
            // 注入 Application 应用层   // .InstancePerLifetimeScope().EnableInterfaceInterceptors().InterceptedBy(typeof(LogAOP));//可以直接替换拦截器;
            builder.RegisterAssemblyTypes(typeof(StaffAppService).GetTypeInfo().Assembly).Where(x => x.FullName.EndsWith("AppService"))
                .AsImplementedInterfaces().PropertiesAutowired();
            #endregion
            #region Infra - Data
            // 注入 Infra - Data 基础设施数据层
            builder.RegisterAssemblyTypes(typeof(EventStoreRepository).GetTypeInfo().Assembly).Where(x => x.FullName.EndsWith("Repository")).AsImplementedInterfaces().PropertiesAutowired();
            //DbContext
            builder.RegisterType<MyContext>().AsSelf().As<DbContext>().InstancePerLifetimeScope();
            //Handler
            builder.RegisterType<InMemoryBus>().As<IMediatorHandler>().InstancePerLifetimeScope();
            //UnitOfWork
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            #endregion
        }

        public static void RegisterServices(IServiceCollection services)
        {

        }
    }
}
