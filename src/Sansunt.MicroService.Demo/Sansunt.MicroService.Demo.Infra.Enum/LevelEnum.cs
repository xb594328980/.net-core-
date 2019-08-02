using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sansunt.MicroService.Demo.Infra.Enum
{
    public enum LevelEnum
    {
        /// <summary>
        /// 低
        /// </summary>
        [Description("低")]
        Low = 1,

        /// <summary>
        /// 中
        /// </summary>
        [Description("中")]
        Intermediate = 2,

        /// <summary>
        /// 高
        /// </summary>
        [Description("高")]
        High = 3,

        /// <summary>
        /// 自定义
        /// </summary>
        [Description("自定义")]
        Custom = 5
    }
}
