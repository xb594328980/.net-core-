using System;
using System.Collections.Generic;
using System.Text;

namespace Sansunt.MicroService.Demo.Application.ViewModels
{
    /// <summary>
    /// Des：
    /// create by xingbo 2019/8/2 17:19:43 
    /// </summary>
    public class StaffViewModel
    {
        public StaffViewModel(Guid id)
        {
            Id = id.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
    }
}
