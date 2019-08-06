using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sansunt.MicroService.Demo.Application.Interfaces;
using Sansunt.MicroService.Demo.Application.ViewModels;

namespace Sansunt.MicroService.Demo.IdentityServer.Host.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        #region MyRegion
        private readonly IStaffAppService _staffAppService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffAppService"></param>
        public ResourceOwnerPasswordValidator(IStaffAppService staffAppService)
        {
            _staffAppService = staffAppService;
        }
        #endregion
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var staffInfo = await _staffAppService.Get(null, context.UserName);
            if (staffInfo != null && staffInfo.Pwd.Equals(context.Password))
            {
                context.Result = new GrantValidationResult(
                  subject: context.UserName,
                  authenticationMethod: "default",
                  claims: GetStaffClaims(staffInfo));
            }
            else
                //验证失败
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "账号密码验证失败");
        }
        //可以根据需要设置相应的Claim
        private Claim[] GetStaffClaims(StaffViewModel user)
        {
            var jsonuserdata = JsonConvert.SerializeObject(user);
            byte[] b = System.Text.Encoding.Default.GetBytes(jsonuserdata);
            byte[] uuid = System.Text.Encoding.Default.GetBytes(Guid.NewGuid().ToString());
            byte[] userId = System.Text.Encoding.Default.GetBytes(user.Id.ToString());
            return new Claim[]
            {
            new Claim("user-id",Convert.ToBase64String(userId)),
            new Claim("token-uuid",Convert.ToBase64String(uuid)),
            new Claim("user-info", Convert.ToBase64String(b))
            };
        }
    }
}
