using System;
using Sansunt.Infra.Tools.Helpers;
using Sansunt.Infra.Tools.Logs.Internal;

namespace Sansunt.Infra.Logs.Exceptionless
{
    /// <summary>
    /// Exceptionless日志上下文
    /// </summary>
    public class LogContext :Tools.Logs.Core.LogContext
    {
        /// <summary>
        /// 创建日志上下文信息
        /// </summary>
        protected override LogContextInfo CreateInfo()
        {
            return new LogContextInfo
            {
                TraceId = Guid.NewGuid().ToString(),
                Stopwatch = GetStopwatch(),
                Url = Web.Url
            };
        }
    }
}
