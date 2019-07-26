using System;
using System.Collections.Generic;
using System.Text;

namespace Sansunt.MicroService.Demo.Extensions
{
    /// <summary>
    /// Des：注册console配置项
    /// create by xingbo 2019/7/26 15:54:19 
    /// </summary>
    public class RegisterConsulOptions
    {
        public HealthService HealthServiceConfig { get; set; }
        public ConsulService ConsulServiceConfig { get; set; }
    }
}
