using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sansunt.Domain.Core.Bus;
using Sansunt.Domain.Core.Notifications;
using Sansunt.Infra.Tools.Helpers;
using Sansunt.Infra.Tools.Logs;
using Sansunt.MicroService.Demo.Domain.Notifications;
using Sansunt.MicroService.Demo.Extensions.App_Helper;
using Sansunt.MicroService.Demo.Extensions.Filters;
using Sansunt.MicroService.Demo.Infra.Enum;

namespace Sansunt.MicroService.Demo.IdentityServer.Host.Controllers
{
    /// <summary>
    /// 父级控制器
    /// <remarks>create by xingbo 18/12/20</remarks>
    /// </summary>
    [EnableCors("AllowSameDomain")]
    [Route("[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ErrorAttribute))]
    [ServiceFilter(typeof(RequestLogAttribute))]
    public class ApiController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="notifications"></param>
        /// <param name="mediator"></param>
        protected ApiController(INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }
        /// <summary>
        /// 获取提醒
        /// </summary>
        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

        /// <summary>
        /// 是否有提醒
        /// </summary>
        /// <returns></returns>
        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

        /// <summary>
        /// 返回信息
        /// </summary>
        /// <param name="result">结果</param>
        /// <returns></returns>
        protected new AjaxResult Response(object result = null)
        {

            if (IsValidOperation())
            {
                return new AjaxResult(result);
                //                return Ok(new
                //                {
                //                    success = true,
                //                    data = result
                //                });
            }
            var error = _notifications.GetNotifications();
            return new AjaxResult(ErrorCodeEnum.OperationFailed, error.Select(n => $"{n.Key}:{n.Value}"));
            //            return BadRequest(new
            //            {
            //                success = false,
            //                errors = _notifications.GetNotifications().Select(n => n.Value)
            //            });
        }

        /// <summary>
        /// 返回错信息
        /// </summary>
        /// <param name="errorCode">错误码</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        protected new AjaxResult ErrorResponse(ErrorCodeEnum errorCode, string errorMsg)
        {
            return new AjaxResult(errorCode, errorMsg);
        }

        /// <summary>
        /// 返回错信息
        /// </summary>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        protected new AjaxResult ErrorResponse(string errorMsg)
        {
            return new AjaxResult(ErrorCodeEnum.OperationFailed, errorMsg);
        }

        /// <summary>
        /// 把验证错误引发错误通知
        /// </summary>
        protected void NotifyModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }
        /// <summary>
        /// 发布错误通知
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        protected void NotifyError(string code, string message)
        {
            _mediator.RaiseEvent(new DomainNotification(code, message));
        }

        /// <summary>
        /// 把身份验证错误引发错误通知
        /// </summary>
        /// <param name="result"></param>
        protected void AddIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                NotifyError(result.ToString(), error.Description);
            }
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
        protected virtual ILog GetLog()
        {
            try
            {
                return Sansunt.Infra.Logs.Log.GetLog(this);
            }
            catch
            {
                return Sansunt.Infra.Logs.Log.Null;
            }
        }
    }
}
