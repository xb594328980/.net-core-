/*
*基础代码由代码生成器自动生成
*生成时间：2019/07/22
*生成者：xingbo
*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Sansunt.MicroService.Demo.Domain.Models;

namespace Sansunt.MicroService.Demo.Infra.Data.Map
{

    /// <summary>
    /// Des:实体类SystemDicConfig.cs 对应数据结构
	/// create by xingbo 2019/07/22
    /// </summary>
    public class DicConfigMap : IEntityTypeConfiguration<DicConfig>
    {
        public void Configure(EntityTypeBuilder<DicConfig> builder)
        {
            builder.ToTable("system_dic_config");//设置表名
            builder.HasKey(x => x.Id);//设置主键
            builder.HasIndex(x => new { x.DicType, x.Key });
            #region 设置列

            //主键
            builder.Property(c => c.Id).HasColumnName("id").IsRequired();

            //字典类型
            builder.Property(c => c.DicType).HasColumnName("dic_type").IsRequired();

            //排序
            builder.Property(c => c.Sort).HasColumnName("sort").IsRequired();

            //名称
            builder.Property(c => c.Name).HasColumnName("name").IsRequired();
            //键
            builder.Property(c => c.Key).HasColumnName("key").IsRequired();
            //值
            builder.Property(c => c.Val).HasColumnName("val").IsRequired();

            //创建人
            builder.Property(c => c.CreateBy).HasColumnName("create_by").IsRequired();

            //创建时间
            builder.Property(c => c.CreateTime).HasColumnName("create_time").IsRequired();

            //编辑时间
            builder.Property(c => c.UpdateTime).HasColumnName("update_time");

            //编辑人
            builder.Property(c => c.UpdateBy).HasColumnName("update_by");

            //说明
            builder.Property(c => c.Remark).HasColumnName("remark");
            #endregion
        }


    }
}
