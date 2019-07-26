using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.IdentityServer.Host.Extensions
{
    public class HealthService
    {
        public string Name { get; set; }

        public string IP { get; set; }

        public int Port { get; set; }

    }
}
