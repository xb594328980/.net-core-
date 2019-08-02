using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using Sansunt.MicroService.Demo.Extensions.App_Helper;

using Sansunt.Infra.Tools;
using Sansunt.Infra.Tools.Helpers;


namespace Sansunt.MicroService.Demo.Extensions.Filters
{
    /// <summary>
    /// 请求日志记录
    /// create by xingbo 19/07/11 
    /// </summary>
    public class RequestLogAttribute : ActionFilterAttribute
    {
        private dynamic _requestDate;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _requestDate = context.ActionArguments;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            var ajaxresult = context.Result as AjaxResult;
            var log = Sansunt.Infra.Logs.Log.GetLog(this);
            //var _auth = new IdentityManager(context.HttpContext, new JwtIdentity());
            if (ajaxresult != null)
            {
                //string ip = _auth.GetRealIp();
                var result = ajaxresult.GetResponseData;
                var request = context.HttpContext.Request;

                StaffRequestInfo info = new StaffRequestInfo();
                // info.Ip = ip;
                info.TargetUrl = request.Path;
                info.RequestType = request.Method;
                info.RequestData = _requestDate;
                info.ResponseData = result;
                //info.UserId = _auth.UserInfo.UserId;
                info.RequestTime = DateTime.Now.ToChineseDateTimeString();
                Console.WriteLine(info.ToJson());
                //log.Debug(info.ToJson());
            }
        }
    }
}
