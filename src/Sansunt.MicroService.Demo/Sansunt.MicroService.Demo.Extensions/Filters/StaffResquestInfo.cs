using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sansunt.MicroService.Demo.Extensions.Filters
{
    /// <summary>
    /// 用户请求信息
    /// </summary>
    public class StaffRequestInfo
    {
        /// <summary>
        /// 请求用户的ip地址
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 目标地址
        /// </summary>
        public string TargetUrl { get; set; }
        /// <summary>
        /// 请求数据
        /// </summary>
        public object RequestData { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object ResponseData { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 请求类型
        /// </summary>
        public string RequestType { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public string RequestTime { get; set; }


    }
}
