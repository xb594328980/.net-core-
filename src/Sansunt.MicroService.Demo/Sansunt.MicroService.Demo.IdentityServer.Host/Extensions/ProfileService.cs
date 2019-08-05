using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sansunt.MicroService.Demo.Application.Interfaces;

namespace Sansunt.MicroService.Demo.IdentityServer.Host.Extensions
{
    /// <summary>
    /// 验证
    /// </summary>
    public class ProfileService : IProfileService
    {
        #region MyRegion
        private readonly IStaffAppService _staffAppService;//自己写的操作数据库Admin表的service
        public ProfileService(IStaffAppService staffAppService)
        {
            _staffAppService = staffAppService;
        }
        #endregion
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                //depending on the scope accessing the user data.
                var claims = context.Subject.Claims.ToList();

                //set issued claims to return
                context.IssuedClaims = claims;
            }
            catch (Exception ex)
            {
                //log your error
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
        }
    }
}
