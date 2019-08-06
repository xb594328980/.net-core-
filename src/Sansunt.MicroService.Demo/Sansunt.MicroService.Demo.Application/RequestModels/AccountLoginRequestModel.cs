using System;
using System.Collections.Generic;
using System.Text;

namespace Sansunt.MicroService.Demo.Application.RequestModels
{
    /// <summary>
    /// Des：
    /// create by xingbo 2019/8/6 10:49:37 
    /// </summary>
    public class AccountLoginRequestModel
    {
        public AccountLoginRequestModel()
        {
            
        }

        public string  Account { get; set; }
        public string  Pwd { get; set; }
    }
}
