/*
*基础代码由代码生成器自动生成
*生成时间：2019/07/22
*生成者：xingbo
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sansunt.MicroService.Demo.Domain.Models
{

    /// <summary>
    /// Des:实体类DicConfig.cs 对应数据表为DicConfig
	/// create by xingbo 2019/07/22
    /// </summary>
    public class DicConfig
    {

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        public String DicType { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public Int32 Sort { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 键
        /// </summary>
        public String Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public String Val { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public Int32 CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 编辑时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 编辑人
        /// </summary>
        public Int32? UpdateBy { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public String Remark { get; set; }

    }
}
