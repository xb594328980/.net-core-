using System;
using System.Collections.Generic;
using System.Text;
using Sansunt.Domain.Core.Commands;

namespace Sansunt.MicroService.Demo.Domain.Commands.Staff
{
    /// <summary>
    /// 用户命令基础
    /// <remarks>create by xingbo 19/05/15</remarks>
    /// </summary>
    public abstract class StaffCommand : Command
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// 用户openId
        /// </summary>
        public string OpenId { get; protected set; }
      
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; protected set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string NickPic { get; protected set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string NickPhone { get; protected set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; protected set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; protected set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; protected set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; protected set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; protected set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; protected set; }


        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; protected set; }

    }
}
