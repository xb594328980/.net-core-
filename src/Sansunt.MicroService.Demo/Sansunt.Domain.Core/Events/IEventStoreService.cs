﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sansunt.Domain.Core.Events
{
    /// <summary>
    /// 领域存储服务接口
    /// <remarks>create by xingbo</remarks>
    /// </summary>
    public interface IEventStoreService
    {
        /// <summary>
        /// 将命令模型进行保存
        /// </summary>
        /// <typeparam name="T"> 泛型：Event命令模型</typeparam>
        /// <param name="theEvent"></param>
        void Save<T>(T theEvent) where T : Event;
    }
}
