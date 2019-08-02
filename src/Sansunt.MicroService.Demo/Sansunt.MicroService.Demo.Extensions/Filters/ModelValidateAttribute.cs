using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sansunt.MicroService.Demo.Extensions.App_Helper;
using Sansunt.Infra.Tools;
using Sansunt.MicroService.Demo.Infra.Enum;

namespace Sansunt.MicroService.Demo.Extensions.Filters
{
    /// <summary>
    /// 模型验证
    /// create by xingbo 19/06/18
    /// </summary>
    public class ModelValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var modelState = filterContext.ModelState;
            if (!modelState.IsValid)
            {
                StringBuilder errorMessage = new StringBuilder();
                foreach (var key in modelState.Keys)
                {
                    var state = modelState[key];
                    if (state.Errors.Any())
                    {
                        errorMessage.Append(state.Errors.Select(x => x.ErrorMessage).Join("", "|") + "|");
                    }
                }
                filterContext.Result = new AjaxResult(ErrorCodeEnum.ParamsError, errorMessage.ToString().TrimEnd('|'));
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
