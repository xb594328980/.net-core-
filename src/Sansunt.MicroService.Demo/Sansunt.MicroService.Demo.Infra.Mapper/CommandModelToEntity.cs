using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Sansunt.MicroService.Demo.Infra.Mapper
{
    /// <summary>
    /// Des:命令转实体，用于编辑时为使部分属性不被默认值覆盖
    /// create by xingbo 2019/5/31 16:17:53
    /// </summary>
    public class CommandModelToEntity : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CommandModelToEntity()
        {
        
        }
    }
}
