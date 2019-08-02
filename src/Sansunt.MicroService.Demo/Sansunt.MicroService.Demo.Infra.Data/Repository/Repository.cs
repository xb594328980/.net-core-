using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
using Sansunt.MicroService.Demo.Domain.Interfaces;
using Sansunt.MicroService.Demo.Infra.Data.Context;

namespace Sansunt.MicroService.Demo.Infra.Data.Repository
{
    /// <summary>
    /// 仓储基类
    /// <remarks>create by xingbo 18/12/19</remarks>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected readonly MyContext Db;
        protected readonly DbSet<TEntity> EntitySet;

        public DbConnection CurrConnection() => Db.Database.GetDbConnection();//获取当前连接

        public IDbContextTransaction CurrTransaction() => Db.Database.BeginTransaction();//获取当前事务
        #region 构造
        public Repository(MyContext context)
        {
            Db = context;
            EntitySet = Db.Set<TEntity>();
        }


        #endregion
        public int SaveChanges() => Db.SaveChanges();
        #region 新增
        public virtual void Add(TEntity obj)
        {
            EntitySet.Add(obj);
        }
        public virtual void Add(IEnumerable<TEntity> objList)
        {
            EntitySet.AddRange(objList);
        }
        #endregion

        public virtual TEntity GetById(params object[] id) => EntitySet.Find(id);

        public virtual IQueryable<TEntity> Gets(Expression<Func<TEntity, bool>> exp, bool ignoreQueryFilters = false)
        {
            if (!ignoreQueryFilters)
                return EntitySet.AsNoTracking().Where(exp);
            else
                return EntitySet.AsNoTracking().IgnoreQueryFilters().Where(exp);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> exp, bool ignoreQueryFilters = false)
        {
            if (!ignoreQueryFilters)
                return EntitySet.AsNoTracking().SingleOrDefault(exp);
            else
                return EntitySet.AsNoTracking().IgnoreQueryFilters().SingleOrDefault(exp);
        }

        public virtual int Count(Expression<Func<TEntity, bool>> exp, bool ignoreQueryFilters = false)
        {
            if (!ignoreQueryFilters)
                return EntitySet.AsNoTracking().Count(exp);
            else
                return EntitySet.AsNoTracking().IgnoreQueryFilters().Count(exp);

        }

        public virtual bool Exist(Expression<Func<TEntity, bool>> exp) => EntitySet.AsNoTracking().Any(exp);

        #region 编辑


        public virtual void Update(params TEntity[] objList)
        {
            EntitySet.UpdateRange(objList);
        }


        public virtual void Update(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            if (propertyExpressions == null || propertyExpressions.Length == 0)
                EntitySet.Update(entity);
            else
            {
                var entry = EntitySet.Attach(entity);
                entry.State = EntityState.Modified;
                foreach (var expression in propertyExpressions)
                    entry.Property(expression).IsModified = true;
            }

        }

        public virtual void UpdateWithOut(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
        {
            if (propertyExpressions == null || propertyExpressions.Length == 0)
                EntitySet.Update(entity);
            else
            {
                var entry = Db.Set<TEntity>().Attach(entity);
                entry.State = EntityState.Modified;
                foreach (var expression in propertyExpressions)
                    entry.Property(expression).IsModified = false;
            }
        }
        #endregion
        #region 删除
        public void Remove(params object[] id)
        {
            EntitySet.Remove(EntitySet.Find(id));
        }

        public void Remove(TEntity entity)
        {
            EntitySet.Remove(entity);
        }

        public virtual void Remove(Expression<Func<TEntity, bool>> whereExpression)
        {
            EntitySet.RemoveRange(EntitySet.Where(whereExpression));
        }

        #endregion
        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }


    }
}
