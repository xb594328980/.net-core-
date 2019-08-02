using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sansunt.MicroService.Demo.Infra.Enum
{
    /// <summary>
    /// 员工登录标志枚举
    /// <remarks>create by xingbo 19/01/03</remarks>
    /// </summary>
    public enum StaffLoginFlagEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disabled = 2
    }
}
