using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroService.IdentityServer.Host.Extensions
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.HasQueryFilter(post => EF.Property<int>(post, "DelFlag") == 0);//设置查询自动添加DelFlag为0的条件
            builder.ToTable("User");//设置表名
            builder.HasKey(x => x.Id);//设置主键
            //实体属性Map
            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .IsRequired();
            builder.Property(c => c.OpenId)
                .HasColumnType("varchar(128)")
                .HasColumnName("OpenId")
                .IsRequired();
            builder.Property(c => c.Gender)
                .HasColumnName("Gender")
                .IsRequired();
            builder.Property(c => c.CreateTime)
                .HasColumnName("CreateTime")
                .IsRequired();
            builder.Property(c => c.NickPhone)
                .HasColumnName("NickPhone").HasColumnType("varchar(64)");
            builder.Property(c => c.NickPic)
                .HasColumnName("NickPic").HasColumnType("varchar(256)");
            builder.Property(c => c.NickName)
                .HasColumnName("NickName").HasColumnType("varchar(64)");
            builder.Property(c => c.City)
                .HasColumnName("City").HasColumnType("varchar(64)");
            builder.Property(c => c.Country)
                .HasColumnName("Country").HasColumnType("varchar(64)");
            builder.Property(c => c.Province)
                .HasColumnName("Province").HasColumnType("varchar(64)");
            builder.Property(c => c.State)
                .IsRequired();

            builder.Property(c => c.UpdateTime);
        }
    }
}
