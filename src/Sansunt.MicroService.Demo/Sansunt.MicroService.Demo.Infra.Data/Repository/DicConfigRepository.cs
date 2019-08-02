/*
*基础代码由代码生成器自动生成
*生成时间：2019/07/22
*生成者：xingbo
*/
using System;
using System.Collections.Generic;
using System.Text;
using Sansunt.MicroService.Demo.Domain.Interfaces;
using Sansunt.MicroService.Demo.Domain.Models;
using Sansunt.MicroService.Demo.Infra.Data.Context;


namespace Sansunt.MicroService.Demo.Infra.Data.Repository
{
    /// <summary>
    /// Des:数据仓储实现
    /// create by xingbo 2019/07/22
    /// </summary>
    public class DicConfigRepository : Repository<DicConfig>, IDicConfigRepository
    {
        public DicConfigRepository(MyContext context) : base(context)
        {
        }
    }
}
