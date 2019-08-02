using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using Sansunt.MicroService.Demo.Infra.Data.Map;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using Sansunt.Infra.Tools.Logs;
using Microsoft.Extensions.Logging;
using Sansunt.Infra.Tools.Helpers;
using Sansunt.MicroService.Demo.Infra.Data.Log;
using Sansunt.MicroService.Demo.Domain.Models;
using Sansunt.MicroService.Demo.Infra.Config;
using Sansunt.MicroService.Demo.Infra.Data.Map;
using Random = Sansunt.Infra.Tools.Helpers.Random;
using Sansunt.MicroService.Demo.Infra.Enum;

namespace Sansunt.MicroService.Demo.Infra.Data.Context
{
    /// <summary>
    /// 权限数据库上下文
    ///  context.Office.IgnoreQueryFilters().Where();//临时禁用查询过滤器
    /// </summary>
    public class MyContext : DbContext
    {

        /// <summary>
        /// 日志工厂
        /// </summary>
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// 配置
        /// </summary>
        private readonly IConfigurationRoot _config;

        /// <summary>
        /// 上下文类型
        /// </summary>
        private readonly ContextTypeEnum _contextType;
        public MyContext(ContextTypeEnum contextType = ContextTypeEnum.Comman) : base()
        {
            _loggerFactory = new LoggerFactory(new[] { new EfLogProvider() });
            // 从 appsetting.json 中获取配置信息
            _config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            _contextType = contextType;
        }



        public DbSet<Staff> Staff { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string connStr = "";
            switch (_contextType)
            {
                case ContextTypeEnum.Read:
                    {
                        var slaveDbs = _config.GetSection("SlaveDb");
                        var slaveDbChildren = slaveDbs.GetChildren()?.ToList();
                        if (slaveDbChildren != null && slaveDbChildren.Any())
                        {
                            int num = new Random().Next(0, slaveDbChildren.Count());
                            connStr = slaveDbChildren[num].Value;
                        }
                        else
                            connStr = slaveDbs.Value;
                        break;
                    }
                case ContextTypeEnum.Comman:
                case ContextTypeEnum.Write:
                    {
                        connStr = _config.GetConnectionString("MasterDb");
                        break;
                    }
            }
            //定义要使用的数据库
            optionsBuilder.UseMySql(connStr);
            EnableLog(optionsBuilder);//配置日志
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StaffMap());
            base.OnModelCreating(modelBuilder);

        }



        /// <summary>
        /// 启用日志
        /// </summary>
        protected void EnableLog(DbContextOptionsBuilder builder)
        {
            var log = GetLog();
            if (IsEnabled(log) == false)
                return;
            builder.EnableSensitiveDataLogging();
            builder.EnableDetailedErrors();
            builder.UseLoggerFactory(_loggerFactory);
        }

        /// <summary>
        /// 获取日志操作
        /// </summary>
        protected virtual ILog GetLog()
        {
            try
            {
                return Sansunt.Infra.Logs.Log.GetLog(EfLog.TraceLogName);
            }
            catch
            {
                return Sansunt.Infra.Logs.Log.Null;
            }
        }

        /// <summary>
        /// 是否启用Ef日志
        /// </summary>
        private bool IsEnabled(ILog log)
        {
            var config = GetConfig();
            if (config.EfLogLevel == EfLogLevel.Off)
                return false;
            if (log.IsTraceEnabled == false)
                return false;
            return true;
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        private EfConfig GetConfig()
        {
            try
            {
                var options = Ioc.Create<ConfigManage>();
                return options.EfConfig;
            }
            catch
            {
                return new EfConfig { EfLogLevel = EfLogLevel.Sql };
            }
        }

    }
}
