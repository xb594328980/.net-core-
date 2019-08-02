using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;

namespace Sansunt.MicroService.Demo.Domain.Interfaces
{
    /// <summary>
    /// 定义泛型仓储接口，并继承IDisposable，显式释放资源
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {

        DbConnection CurrConnection();

        IDbContextTransaction CurrTransaction();
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="obj"></param>
        void Add(TEntity obj);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="objList"></param>
        void Add(IEnumerable<TEntity> objList);
        /// <summary>
        /// 根据id获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(params object[] id);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <param name="ignoreQueryFilters">是否禁用查询过滤器</param>
        /// <returns></returns>
        IQueryable<TEntity> Gets(Expression<Func<TEntity, bool>> exp, bool ignoreQueryFilters = false);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <param name="ignoreQueryFilters">是否禁用查询过滤器</param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> exp, bool ignoreQueryFilters = false);

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <param name="ignoreQueryFilters">是否禁用查询过滤器</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> exp, bool ignoreQueryFilters = false);
        /// <summary>
        /// 根据对象进行更新
        /// </summary>
        /// <param name="objList"></param>
        void Update(params TEntity[] objList);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyExpressions">properties  to update</param>
        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="propertyExpressions">properties not to update</param>
        void UpdateWithOut(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        void Remove(params object[] id);


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// 按条件批量删除
        /// </summary>
        /// <param name="exp"></param>
        void Remove(Expression<Func<TEntity, bool>> exp);
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
