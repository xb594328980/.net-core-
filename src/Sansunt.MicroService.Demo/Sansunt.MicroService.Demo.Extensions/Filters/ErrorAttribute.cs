using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sansunt.MicroService.Demo.Extensions.App_Helper;
using Sansunt.Infra.Tools.Helpers;
using Sansunt.Infra.Tools.Logs;
using Sansunt.MicroService.Demo.Infra.Enum;

namespace Sansunt.MicroService.Demo.Extensions.Filters
{
    /// <summary>
    ///
    /// </summary>
    public class ErrorAttribute : ExceptionFilterAttribute
    {
        //private readonly IdentityManager _identityManager;

        private ExceptionContext _filterContext;
        //public ErrorAttribute(IdentityManager identityManager)
        //{
        //    _identityManager = identityManager;
        //}

        public override void OnException(ExceptionContext filterContext)
        {
            _filterContext = filterContext;
            //获取异常信息，入库保存
            Exception error = filterContext.Exception;
            string message = error.Message;//错误信息
            string url = filterContext.HttpContext.Request.Path;//错误发生地址
            string ip = GetRealIp(filterContext);
            filterContext.ExceptionHandled = true;
            string str = "";
            //if (_identityManager.Logined())
            //{
            //    var user = _identityManager.UserInfo;
            //    str = string.Format("▷时间：{2}◆「{3}」◆「{0}（{1}）」◆操作地址：{4}\n◆错误信息:{5}◁", user.Account, user.UserId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ip, url, message);
            //}
            //else
            //{
            str = string.Format("▷时间：{0}◆「{1}」◆操作地址：{2}\n◆错误信息:{3}◁", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ip, url, message);
            //}
            Log.Error(str);
            filterContext.Result = new AjaxResult(ErrorCodeEnum.SystemException, str);
            filterContext.ExceptionHandled = true;
        }

        /// <summary>
        /// 获取客户端ip地址
        /// </summary>
        /// <returns></returns>
        public string GetRealIp(ExceptionContext filterContext)
        {
            string ip = filterContext.HttpContext.Request.Headers["X-Real-IP"];
            if (string.IsNullOrEmpty(ip))
                ip = filterContext.HttpContext.Connection.RemoteIpAddress.ToString();
            return ip;
        }

        /// <summary>
        /// 日志
        /// </summary>
        private ILog _log;

        /// <summary>
        /// 日志
        /// </summary>
        public ILog Log => _log ?? (_log = GetLog());

        /// <summary>
        /// 获取日志操作
        /// </summary>
        protected virtual ILog GetLog(object instance = null)
        {
            try
            {
                return Sansunt.Infra.Logs.Log.GetLog(instance ?? this);
            }
            catch
            {
                return Sansunt.Infra.Logs.Log.Null;
            }
        }
    }
}