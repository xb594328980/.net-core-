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
            builder.ToTable("sys_staff");//设置表名
            builder.HasKey(x => x.Id);//设置主键
            builder.HasIndex(x => new { x.Account, x.DelFlag, x.Id });
            #region 设置列

            builder.Property(c => c.Id).HasColumnName("Id").HasColumnType("varchar(64)").IsRequired();

            builder.Property(c => c.Account).HasColumnName("account").HasColumnType("varchar(64)").IsRequired();
            builder.Property(c => c.NickName).HasColumnName("nick_name").HasColumnType("varchar(64)").IsRequired();
            builder.Property(c => c.Pwd).HasColumnName("pwd").HasColumnType("varchar(64)").IsRequired();
            builder.Property(c => c.Status).HasColumnName("status").IsRequired();

            builder.Property(c => c.CreateBy).HasColumnName("create_by").HasColumnType("varchar(64)").IsRequired();
            builder.Property(c => c.UpdateBy).HasColumnName("update_by").HasColumnType("varchar(64)");
            builder.Property(c => c.CreateTime).HasColumnName("create_time").IsRequired();
            builder.Property(c => c.UpdateTime).HasColumnName("update_time");
            builder.Property(c => c.Remark).HasColumnType("varchar(512)").HasColumnName("remark");
            builder.Property(c => c.DelFlag).HasColumnName("del_flag");
            #endregion
        }
    }
}
