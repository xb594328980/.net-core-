using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.IdentityServer.Host.Extensions
{
    public interface IUserService
    {
        Task<User> GetByStr(string account, string pwd);//根据用户名和密码查找用户

        Task<User> Get(int id);
    }
}
