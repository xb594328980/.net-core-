using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MediatR;
using Sansunt.Domain.Core.Events;
using Sansunt.Domain.Core.Notifications;

using Sansunt.Infra.Tools.Dependency;
using Sansunt.MicroService.Demo.Application.EventSourcing;
using Sansunt.MicroService.Demo.Domain.Interfaces;
using Sansunt.MicroService.Demo.Infra.Data.Context;
using Sansunt.MicroService.Demo.Infra.Data.Repository;

namespace Sansunt.MicroService.Demo.Infra.Ioc
{
    /// <summary>
    /// 事件注册
    /// <remarks>
    /// create by xingbo 19/05/15
    /// </remarks>
    /// </summary>
    public class EventBootStrapper : Module, IConfig
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<CardEventHandler>().As<INotificationHandler<UpdateCardAndCardHolderEvent>>();

            //event sourceing
            builder.RegisterType<SqlEventStoreService>().As<IEventStoreService>();
            builder.RegisterType<EventStoreRepository>().As<IEventStoreRepository>();
            builder.RegisterType<SQLEventStoreContext>().InstancePerLifetimeScope();
        }
    }
}
