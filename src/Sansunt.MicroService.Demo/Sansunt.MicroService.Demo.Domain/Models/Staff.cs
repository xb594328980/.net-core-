using System;
using System.Collections.Generic;
using System.Text;
using Sansunt.Domain.Core.Models;

namespace Sansunt.MicroService.Demo.Domain.Models
{
    /// <summary>
    /// Des：用户
    /// create by xingbo 2019/8/2 13:26:54 
    /// </summary>
    public class Staff : Entity
    {

        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }


        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public Guid CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 编辑用户
        /// </summary>
        public Guid? UpdateBy { get; set; }

        /// <summary>
        /// 编辑时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 删除标志
        /// </summary>
        public int DelFlag { get; set; }



    }
}
