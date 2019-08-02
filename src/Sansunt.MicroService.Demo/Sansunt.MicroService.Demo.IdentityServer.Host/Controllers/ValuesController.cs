using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sansunt.Domain.Core.Bus;
using Sansunt.Domain.Core.Notifications;
using Sansunt.Infra.Logs;
using Sansunt.Infra.Tools.Caches;
using Sansunt.Infra.Tools.Schedulers;

namespace Sansunt.MicroService.Demo.IdentityServer.Host.Controllers
{
    public class ValuesController : ApiController
    {
        #region 初始化
        private readonly ICache _cache;


        public ValuesController(INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator,
            ICache cache) : base(notifications, mediator)
        {
            _cache = cache;
        }


        #endregion
        /// <summary>
        /// 示例
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> Get()
        {
            Log.Debug("23123123213");
            return ("abc");
        }


    }
}
