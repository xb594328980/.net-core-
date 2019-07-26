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

namespace MicroService.IdentityServer.Host.Extensions
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private IUserService _userService;//自己写的操作数据库Admin表的service
        public ResourceOwnerPasswordValidator(IUserService userService)
        {
            _userService = userService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            User user = await _userService.GetByStr(context.UserName, context.Password);
            if (user != null)
            {
                context.Result = new GrantValidationResult(
                  subject: context.UserName,
                  authenticationMethod: "default",
                  claims: GetUserClaims(user));
            }
            else
            {
                //验证失败
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "账号密码验证失败");
            }
        }
        //可以根据需要设置相应的Claim
        private Claim[] GetUserClaims(User user)
        {
            var jsonuserdata = JsonConvert.SerializeObject(user);
            byte[] b = System.Text.Encoding.Default.GetBytes(jsonuserdata);
            byte[] uuid = System.Text.Encoding.Default.GetBytes(Guid.NewGuid().ToString());
            return new Claim[]
            {
            new Claim("user-id",user.Id.ToString()),
            new Claim("token-uuid",Convert.ToBase64String(uuid)),
            new Claim("user-info", Convert.ToBase64String(b))
            };
        }
    }
}
