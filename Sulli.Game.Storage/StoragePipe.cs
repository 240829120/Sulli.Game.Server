using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储管道
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TKey">键</typeparam>
    public class StoragePipe<TEntity, TKey> : IStoragePipe<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 层
        /// </summary>
        public List<IStorageLayer<TEntity, TKey>> Layers { get; private set; } = new List<IStorageLayer<TEntity, TKey>>();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context">仓储上下文</param>
        public async Task InitializationAsync(IStorageContext context)
        {
            IStorageLayer<TEntity, TKey>? last_layer = this.Layers.LastOrDefault();
            if (last_layer == null)
                return;

            StorageResult<TEntity, TKey> result = await last_layer.FindAsync(StorageQuery<TEntity, TKey>.None, context);
            if (!result.IsSuccess)
            {
                throw new StorageInitializationException(last_layer, result.Message, result.Exception);
            }

            if (this.Layers.Count == 1)
                return;

            for (int i = this.Layers.Count - 2; i >= 0; --i)
            {
                IStorageLayer<TEntity, TKey> layer = this.Layers[i];
                StorageResult innerResult = await layer.InsertManyAsync(result.Result, context);

                if (!innerResult.IsSuccess)
                {
                    throw new StorageInitializationException(layer, result.Message, result.Exception);
                }
            }
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="context">仓储上下文</param>
        public async Task<StorageResult> InsertAsync(TEntity entity, IStorageContext context)
        {
            foreach (IStorageLayer<TEntity, TKey> layer in this.Layers)
            {
                StorageResult result = await layer.InsertAsync(entity, context);

                if (!result.IsSuccess)
                    return result;
            }

            return StorageResult.Success();
        }

        /// <summary>
        /// 插入多个
        /// </summary>
        /// <param name="entitys">实体集合</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>仓储返回</returns>
        public async Task<StorageResult> InsertManyAsync(IEnumerable<TEntity> entitys, IStorageContext context)
        {
            foreach (IStorageLayer<TEntity, TKey> layer in this.Layers)
            {
                StorageResult result = await layer.InsertManyAsync(entitys, context);

                if (!result.IsSuccess)
                    return result;
            }

            return StorageResult.Success();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="where">条件</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>仓储返回</returns>
        public async Task<StorageResult> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> where, IStorageContext context)
        {
            foreach (IStorageLayer<TEntity, TKey> layer in this.Layers)
            {
                StorageResult result = await layer.UpdateAsync(entity, where, context);

                if (!result.IsSuccess)
                    return result;
            }

            return StorageResult.Success();
        }

        /// <summary>
        /// 插入或更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="where">条件</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>仓储返回</returns>
        public async Task<StorageResult> UpsertAsync(TEntity entity, Expression<Func<TEntity, bool>> where, IStorageContext context)
        {
            foreach (IStorageLayer<TEntity, TKey> layer in this.Layers)
            {
                StorageResult result = await layer.UpsertAsync(entity, where, context);

                if (!result.IsSuccess)
                    return result;
            }

            return StorageResult.Success();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>查询结果</returns>
        public async Task<StorageResult<TEntity, TKey>> FindAsync(StorageQuery<TEntity, TKey> query, IStorageContext context)
        {
            return await this.ExecuteFindAsync(layer => layer.FindAsync(query, context));
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>查询结果</returns>
        public async Task<StoragePageResult<TEntity, TKey>> PageAsync(StoragePageQuery<TEntity, TKey> query, IStorageContext context)
        {
            return await this.ExecutePageAsync(query.Page, query.PageSize, layer => layer.PageAsync(query, context));
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="func">查询数据方法</param>
        /// <returns>查询结果</returns>
        private async Task<StorageResult<TEntity, TKey>> ExecuteFindAsync(Func<IStorageLayer<TEntity, TKey>, Task<StorageResult<TEntity, TKey>>> func)
        {
            StorageResult<TEntity, TKey> result = new StorageResult<TEntity, TKey>();

            // 分层查询
            foreach (IStorageLayer<TEntity, TKey> layer in this.Layers)
            {
                result = await func(layer);
                if (!result.IsSuccess)
                {
                    return StorageResult<TEntity, TKey>.Fail($"storage pipe find error, type:{typeof(TEntity).Name}");
                }

                if (result.Result.Count == 0)
                {
                    continue;
                }

                break;
            }

            return result;
        }

        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="page">页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="func">查询数据方法</param>
        /// <returns>查询结果</returns>
        private async Task<StoragePageResult<TEntity, TKey>> ExecutePageAsync(int page, int pageSize, Func<IStorageLayer<TEntity, TKey>, Task<StoragePageResult<TEntity, TKey>>> func)
        {
            StoragePageResult<TEntity, TKey> result = new StoragePageResult<TEntity, TKey>();

            // 分层查询
            foreach (IStorageLayer<TEntity, TKey> layer in this.Layers)
            {
                result = await func(layer);
                if (!result.IsSuccess)
                {
                    return StoragePageResult<TEntity, TKey>.Fail($"storage pipe find error, type:{typeof(TEntity).Name}");
                }

                if (result.Result.Count == 0)
                {
                    continue;
                }

                break;
            }

            return result;
        }
    }
}
