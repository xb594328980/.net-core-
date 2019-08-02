using System;
using System.Collections.Generic;
using System.Text;
using Sansunt.MicroService.Demo.Domain.Interfaces;
using Sansunt.MicroService.Demo.Infra.Data;
using Sansunt.MicroService.Demo.Infra.Data.Context;

namespace Sansunt.MicroService.Demo.Infra.Data.UoW
{
    /// <summary>
    /// 工作单元类
    /// <remarks>create by  xingbo 18/12/19</remarks>
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        //数据库上下文
        private readonly MyContext _context;

        /// <summary>
        /// 跟踪号
        /// </summary>
        public string TraceId { get; set; }

        //构造函数注入
        public UnitOfWork(MyContext context)
        {
            _context = context;
            TraceId = Guid.NewGuid().ToString();
        }

        //上下文提交
        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        //手动回收
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
