using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储管道扩展
    /// </summary>
    public static class StoragePipeExtend
    {
        /// <summary>
        /// 查询一个
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="pipe">管道</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>查询结果</returns>
        public static async Task<StorageResult<TEntity, TKey>> FindOneAsync<TEntity, TKey>(this IStoragePipe<TEntity, TKey> pipe, IStorageContext context)
            where TEntity : EntityBase<TKey>
            where TKey : IEquatable<TKey>
        {
            StorageQuery<TEntity, TKey> query = new StorageQuery<TEntity, TKey>();
            query.Limit = 1;

            return await pipe.FindAsync(query, context);
        }

        /// <summary>
        /// 查询一个
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="pipe">管道</param>
        /// <param name="where">条件</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>查询结果</returns>
        public static async Task<StorageResult<TEntity, TKey>> FindOneAsync<TEntity, TKey>(this IStoragePipe<TEntity, TKey> pipe, Expression<Func<TEntity, bool>> where, IStorageContext context)
            where TEntity : EntityBase<TKey>
            where TKey : IEquatable<TKey>
        {
            StorageQuery<TEntity, TKey> query = new StorageQuery<TEntity, TKey>();
            query.Where = where;
            query.Limit = 1;

            return await pipe.FindAsync(query, context);
        }

        /// <summary>
        /// 查询一个
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="pipe">管道</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>查询结果</returns>
        public static async Task<StorageResult<TEntity, TKey>> FindOneAsync<TEntity, TKey>(this IStoragePipe<TEntity, TKey> pipe, Expression<Func<TEntity, bool>> where, StorageOrderOption<TEntity, TKey> order, IStorageContext context)
            where TEntity : EntityBase<TKey>
            where TKey : IEquatable<TKey>
        {
            StorageQuery<TEntity, TKey> query = new StorageQuery<TEntity, TKey>();
            query.Where = where;
            query.Limit = 1;
            query.OrderOptions.Add(order);

            return await pipe.FindAsync(query, context);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="pipe">管道</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>查询结果</returns>
        public static async Task<StorageResult<TEntity, TKey>> FindAsync<TEntity, TKey>(this IStoragePipe<TEntity, TKey> pipe, IStorageContext context)
            where TEntity : EntityBase<TKey>
            where TKey : IEquatable<TKey>
        {
            return await pipe.FindAsync(StorageQuery<TEntity, TKey>.None, context);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="pipe">管道</param>
        /// <param name="where">条件</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>查询结果</returns>
        public static async Task<StorageResult<TEntity, TKey>> FindAsync<TEntity, TKey>(this IStoragePipe<TEntity, TKey> pipe, Expression<Func<TEntity, bool>> where, IStorageContext context)
            where TEntity : EntityBase<TKey>
            where TKey : IEquatable<TKey>
        {
            StorageQuery<TEntity, TKey> query = new StorageQuery<TEntity, TKey>();
            query.Where = where;

            return await pipe.FindAsync(query, context);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="pipe">管道</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>查询结果</returns>
        public static async Task<StorageResult<TEntity, TKey>> FindAsync<TEntity, TKey>(this IStoragePipe<TEntity, TKey> pipe, Expression<Func<TEntity, bool>> where, StorageOrderOption<TEntity, TKey> order, IStorageContext context)
            where TEntity : EntityBase<TKey>
            where TKey : IEquatable<TKey>
        {
            StorageQuery<TEntity, TKey> query = new StorageQuery<TEntity, TKey>();
            query.Where = where;
            query.OrderOptions.Add(order);

            return await pipe.FindAsync(query, context);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="pipe">管道</param>
        /// <param name="entity">实体</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>仓储返回</returns>
        public static async Task<StorageResult> UpdateAsync<TEntity, TKey>(this IStoragePipe<TEntity, TKey> pipe, TEntity entity, IStorageContext context)
            where TEntity : EntityBase<TKey>
            where TKey : IEquatable<TKey>
        {
            return await pipe.UpdateAsync(entity, p => p.ID.Equals(entity.ID), context);
        }

        /// <summary>
        /// 插入或更新
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="pipe">管道</param>
        /// <param name="entity">实体</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>仓储返回</returns>
        public static async Task<StorageResult> UpsertAsync<TEntity, TKey>(this IStoragePipe<TEntity, TKey> pipe, TEntity entity, IStorageContext context)
            where TEntity : EntityBase<TKey>
            where TKey : IEquatable<TKey>
        {
            return await pipe.UpsertAsync(entity, p => p.ID.Equals(entity.ID), context);
        }
    }
}

