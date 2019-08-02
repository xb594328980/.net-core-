using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sansunt.MicroService.Demo.Infra.Enum
{
    /// <summary>
    /// 排序类型
    /// create by xingbo 19/07/1
    /// </summary>
    public enum OrderTypeEnum
    {
        /// <summary>
        /// 升序
        /// </summary>
        [Description("升序")]
        Asc = 1,
        /// <summary>
        /// 降序
        /// </summary>
        [Description("降序")]
        Desc = 2
    }
}
