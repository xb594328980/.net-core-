using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Sansunt.MicroService.Demo.Application.ViewModels
{
    /// <summary>
    /// Des：
    /// create by xingbo 2019/8/2 17:19:43 
    /// </summary>
    public class StaffViewModel
    {

        /// <summary>
        /// 账户
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 密码
        /// 密码不能返回前端，故在序列化返回时忽略此字段
        /// </summary>
        [JsonIgnore]
        public string Pwd { get; set; }


        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }


        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }



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
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
