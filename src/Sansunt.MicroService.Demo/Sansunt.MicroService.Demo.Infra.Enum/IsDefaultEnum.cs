using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sansunt.MicroService.Demo.Infra.Enum
{
    /// <summary>
    /// 是否默认枚举
    /// create by xingbo 19/06/19
    /// </summary>
    public enum IsDefaultEnum
    {
        /// <summary>
        /// 默认
        /// </summary>
        [Description("默认")]
        Default = 1,
        /// <summary>
        /// 非默认
        /// </summary>
        [Description("非默认")]
        NonDefault = 0
    }
}
