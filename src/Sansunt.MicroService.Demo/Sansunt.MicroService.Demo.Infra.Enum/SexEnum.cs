using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sansunt.MicroService.Demo.Infra.Enum
{
    /// <summary>
    /// 性别枚举
    /// create by xingbo 19/06/19
    /// </summary>
    public enum SexEnum
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Description("未设置")]
        None = 0,
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Man = 1,
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Woman = 2
    }
}
