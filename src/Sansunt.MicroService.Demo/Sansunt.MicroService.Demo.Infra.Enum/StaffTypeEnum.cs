using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sansunt.MicroService.Demo.Infra.Enum
{
    /// <summary>
    /// 员工类型枚举
    /// <remarks>create by xingbo 19/01/03</remarks>
    /// </summary>
    public enum StaffTypeEnum
    {
        /// <summary>
        /// 系统管理员
        /// </summary>
        [Description("系统管理员")]
        Admin = 1,
        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")]
        System = 2,
        /// <summary>
        /// 普通员工
        /// </summary>
        [Description("普通员工")]
        Normal = 3,
        /// <summary>
        /// 自定义
        /// </summary>
        [Description("自定义")]
        Custom = 4
    }
}
