using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroService.IdentityServer.Host.Extensions
{
    public class AppContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connStr = "Data Source=192.168.31.218;Initial Catalog=HiCard;User Id=sa;password=jsxh123!@#;";
            //定义要使用的数据库
            optionsBuilder.UseSqlServer(connStr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            base.OnModelCreating(modelBuilder);

        }

        #region 实体集
        public DbSet<User> User { get; set; }//注意 这里这个Admin不能写成Admins否则会报错找不到Admins 因为我们现在数据库和表是现成的 这里就相当于实体对应的数据库是Admin
        #endregion
    }
}
