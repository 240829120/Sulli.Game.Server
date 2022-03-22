using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IStorage<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>仓储返回</returns>
        Task<StorageResult> InsertAsync(TEntity entity, IStorageContext context);

        /// <summary>
        /// 插入多个
        /// </summary>
        /// <param name="entitys">实体集合</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>仓储返回</returns>
        Task<StorageResult> InsertManyAsync(IEnumerable<TEntity> entitys, IStorageContext context);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="where">条件</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>仓储返回</returns>
        Task<StorageResult> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> where, IStorageContext context);

        /// <summary>
        /// 插入或更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="where">条件</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>仓储返回</returns>
        Task<StorageResult> UpsertAsync(TEntity entity, Expression<Func<TEntity, bool>> where, IStorageContext context);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>查询结果</returns>
        Task<StorageResult<TEntity, TKey>> FindAsync(StorageQuery<TEntity, TKey> query, IStorageContext context);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>查询结果</returns>
        Task<StoragePageResult<TEntity, TKey>> PageAsync(StoragePageQuery<TEntity, TKey> query, IStorageContext context);

    }
}
