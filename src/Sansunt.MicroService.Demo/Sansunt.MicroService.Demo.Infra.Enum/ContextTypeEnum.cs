using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sansunt.MicroService.Demo.Infra.Enum
{
    /// <summary>
    /// Des：上下文类型
    /// create by xingbo 2019/7/12 10:34:50 
    /// </summary>
    public enum ContextTypeEnum
    {
        /// <summary>
        /// 读库
        /// </summary>
        [Description("读库")]
        Read = 1,
        /// <summary>
        /// 写库
        /// </summary>
        [Description("写库")]
        Write = 2,
        /// <summary>
        /// 通用
        /// </summary>
        [Description("通用")]
        Comman = 3
    }
}
