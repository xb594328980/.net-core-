﻿using Sansunt.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Sansunt.Infra.Tools.Events;
using Sansunt.Infra.Tools.Helpers;
using Sansunt.MicroService.Demo.Domain.Interfaces;
using Event = Sansunt.Domain.Core.Events.Event;

namespace Sansunt.MicroService.Demo.Application.EventSourcing
{
    /// <summary>
    /// 事件存储服务类
    /// <remarks>create by xingbo</remarks>
    /// </summary>
    public class SqlEventStoreService : IEventStoreService
    {
        // 注入我们的仓储接口
        private readonly IEventStoreRepository _eventStoreRepository;

        public SqlEventStoreService(IEventStoreRepository eventStoreRepository)
        {

            _eventStoreRepository = eventStoreRepository;
        }

        /// <summary>
        /// 保存事件模型统一方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="theEvent"></param>
        public void Save<T>(T theEvent) where T : Event
        {
            // 对事件模型序列化
            var serializedData = Json.ToJson(theEvent);

            var eventStored = new EventStore(
                theEvent,
                serializedData,
                "Sansunt.HiCard.Application");
            _eventStoreRepository.Store(eventStored);
        }
    }
}
