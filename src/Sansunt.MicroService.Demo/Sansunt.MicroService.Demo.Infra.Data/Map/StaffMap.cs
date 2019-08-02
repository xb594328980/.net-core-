using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sansunt.MicroService.Demo.Domain.Models;

namespace Sansunt.MicroService.Demo.Infra.Data.Map
{
    /// <summary>
    /// Des：员工数据结构
    /// create by xingbo 2019/8/2 13:33:04 
    /// </summary>
    public class StaffMap : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            throw new NotImplementedException();
        }
    }
}
