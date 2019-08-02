using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sansunt.MicroService.Demo.Infra.Enum;

namespace Sansunt.MicroService.Demo.Extensions.App_Helper
{
    /// <summary>
    /// 分页请求参数
    /// create by xingbo 
    /// </summary>
    public class PageRequestModel<T> where T : struct
    {
        /// <summary>
        /// 获取数据开始位置
        /// </summary>
        public int Start { get; set; } = 0;

        /// <summary>
        /// 获取数据长度，当为-1时则为获取全部
        /// </summary>
        public int Length { get; set; } = 20;
        /// <summary>
        /// 排序字段,需包含值为0的枚举
        /// </summary>
        public T SortBy { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public OrderTypeEnum OrderBy { get; set; } = OrderTypeEnum.Desc;

        /// <summary>
        /// 获取分页参数
        /// </summary>
        /// <param name="take">获取数据条目</param>
        /// <param name="skip">跳过数据量</param>
        /// <param name="allowGetAll">是否允许获取全部 默认为false</param>
        /// <returns>是否分页，根据Length判断，-1为不分页获取全部 </returns>
        public void PageParams(out int? take, out int skip, bool allowGetAll = false)
        {
            skip = Start > 0 ? (Start - 1) : 0;
            take = null;//初始化为获取全部
            if (Length > 0)//如length为有效值则以length为准
                take = Length;
            else if (!allowGetAll && Length < 0)//排除不允许获取全部，而参数传递获取全部的情况，赋默认值
                take = 20;
        }
    }
}
