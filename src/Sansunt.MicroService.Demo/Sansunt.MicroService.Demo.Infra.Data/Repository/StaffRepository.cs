using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

using Sansunt.MicroService.Demo.Domain.Interfaces;
using Sansunt.MicroService.Demo.Domain.Models;
using Sansunt.MicroService.Demo.Infra.Data.Context;

namespace Sansunt.MicroService.Demo.Infra.Data.Repository
{
    /// <summary>
    /// 员工仓储
    /// <remarks>create by xingbo 19/1/3</remarks>
    /// </summary>
    public class StaffRepository : Repository<Staff>, IStaffRepository
    {
        public StaffRepository(MyContext context) : base(context)
        {
        }
    }
}
