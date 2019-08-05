using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sansunt.MicroService.Demo.Application.ViewModels;

namespace Sansunt.MicroService.Demo.Application.Interfaces
{
    /// <summary>
    /// 用户服务定义
    /// <remarks>create by xingbo 19/05/15</remarks>
    /// </summary>
    public interface IStaffAppService : IDisposable
    {
        /// <summary>
        /// 按照账户或id查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<StaffViewModel> Get(Guid? id, string account);

    }
}
