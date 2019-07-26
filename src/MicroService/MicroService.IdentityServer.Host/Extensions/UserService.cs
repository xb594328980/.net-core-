using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.IdentityServer.Host.Extensions
{
    public class UserService : IUserService
    {
        public AppContext db;
        public UserService(AppContext _efContext)
        {
            db = _efContext;
        }

        public async Task<User> Get(int id)
        {
            User m = await db.User.Where(a => a.Id == id).SingleOrDefaultAsync();
            if (m != null)
                return m;
            else
                return null;
        }

        public async Task<User> GetByStr(string account, string pwd)
        {
            User m = await db.User.Where(a => a.OpenId == account).SingleOrDefaultAsync();
            if (m != null)
                return m;
            else
                return null;
        }
    }
}
