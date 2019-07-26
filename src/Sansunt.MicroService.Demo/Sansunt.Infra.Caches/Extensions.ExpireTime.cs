using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sansunt.Infra.Caches
{
    /// <summary>
    /// 为CacheExpireType增加扩展方法
    /// create by xingbo 19/06/13
    /// </summary>
    public static class ExpireTime
    {
        /// <summary>
        /// 获取时间间隔
        /// create by xingbo 19/06/13
        /// </summary>
        /// <param name="exType">过期类型</param>
        /// <returns></returns>
        public static TimeSpan GetExpireTime(this CacheExpireType exType)
        {
            switch (exType)
            {
                case CacheExpireType.Minute1:
                    return new TimeSpan(0, 1, 0);
                case CacheExpireType.Minute5:
                    return new TimeSpan(0, 5, 0);
                case CacheExpireType.Hour1:
                    return new TimeSpan(1, 0, 0);
                case CacheExpireType.Hour4:
                    return new TimeSpan(4, 0, 0);
                case CacheExpireType.Day1:
                    return new TimeSpan(1, 0, 0, 0);
                case CacheExpireType.Always:
                case CacheExpireType.Invariable:
                    return new TimeSpan(365, 0, 0, 0);
                case CacheExpireType.Stable:
                    return new TimeSpan(8, 0, 0);
                case CacheExpireType.Usual:
                    return new TimeSpan(1, 0, 0);
                case CacheExpireType.RelativelyUsual:
                    return new TimeSpan(0, 10, 0);
                case CacheExpireType.Temporary:
                    return new TimeSpan(0, 5, 0);
                default:
                    return new TimeSpan(0, 5, 0);
            }
        }
    }
}
