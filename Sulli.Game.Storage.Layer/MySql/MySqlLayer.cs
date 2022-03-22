using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kogel.Dapper.Extension;
using Kogel.Dapper.Extension.MySql;
using Kogel.Dapper.Extension.Core;

namespace Sulli.Game.Storage.Layer
{
    /// <summary>
    /// MySql仓储层
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TKey">键</typeparam>
    public class MySqlLayer<TEntity, TKey> : StorageLayerBase, IStorageLayer<TEntity, TKey> where TEntity : EntityBase<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// MySql仓储层
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="option">设置</param>
        public MySqlLayer(string name, MySqlLayerOption option) : base(name)
        {
            this.Option = option;
        }

        /// <summary>
        /// 设置
        /// </summary>
        public MySqlLayerOption Option { get; private set; }

        /// <summary>
        /// 锁对象
        /// </summary>
        private ReaderWriterLockSlim lock_object = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>仓储返回</returns>
        public async Task<StorageResult> InsertAsync(TEntity entity, IStorageContext context)
        {
            StorageContextBase? contextBase = context as StorageContextBase;
            if (contextBase == null)
                return await Task.FromResult(StorageResult.Fail("context is not StorageContextBase"));

            var comm = contextBase.MySqlConnection.CommandSet<TEntity>();
            int result = await comm.InsertAsync(entity);

            return result == 1 ? StorageResult.Success() : StorageResult.Fail("insert error.");
        }

        /// <summary>
        /// 插入多个
        /// </summary>
        /// <param name="entitys">实体集合</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>仓储返回</returns>
        public async Task<StorageResult> InsertManyAsync(IEnumerable<TEntity> entitys, IStorageContext context)
        {
            StorageContextBase? contextBase = context as StorageContextBase;
            if (contextBase == null)
                return await Task.FromResult(StorageResult.Fail("context is not StorageContextBase"));

            var comm = contextBase.MySqlConnection.CommandSet<TEntity>();
            int result = await comm.InsertAsync(entitys);

            return result == 1 ? StorageResult.Success() : StorageResult.Fail("insert error.");
        }

        /// <summary>
        /// 插入或更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="where">条件</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>仓储返回</returns>
        public async Task<StorageResult> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> where, IStorageContext context)
        {
            StorageContextBase? contextBase = context as StorageContextBase;
            if (contextBase == null)
                return await Task.FromResult(StorageResult.Fail("context is not StorageContextBase"));

            var comm = contextBase.MySqlConnection.CommandSet<TEntity>();
            int result = await comm.Where(where).UpdateAsync(entity);

            return result == 1 ? StorageResult.Success() : StorageResult.Fail("insert error.");
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
            StorageContextBase? contextBase = context as StorageContextBase;
            if (contextBase == null)
                return await Task.FromResult(StorageResult.Fail("context is not StorageContextBase"));

            var queryComm = contextBase.MySqlConnection.QuerySet<TEntity>();
            var query = await queryComm.Where(where).GetAsync();
            if (query == null)
            {
                var comm = contextBase.MySqlConnection.CommandSet<TEntity>();
                int result = await comm.InsertAsync(entity);

                return result == 1 ? StorageResult.Success() : StorageResult.Fail("insert error.");
            }
            else if (entity.ID.Equals(query.ID))
            {
                var comm = contextBase.MySqlConnection.CommandSet<TEntity>();
                int result = await comm.UpdateAsync(entity);

                return result == 1 ? StorageResult.Success() : StorageResult.Fail("insert error.");
            }
            else
            {
                return StorageResult.Fail("execute where filter error.");
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>查询结果</returns>
        public async Task<StorageResult<TEntity, TKey>> FindAsync(StorageQuery<TEntity, TKey> query, IStorageContext context)
        {
            StorageContextBase? contextBase = context as StorageContextBase;
            if (contextBase == null)
                return await Task.FromResult(StorageResult<TEntity, TKey>.Fail("context is not StorageContextBase"));

            var comm = contextBase.MySqlConnection.QuerySet<TEntity>();
            // Where
            var q = comm.Where(query.Where);
            // Order
            if (query.OrderOptions != null && query.OrderOptions.Count > 0)
            {
                foreach (var order in query.OrderOptions)
                {
                    switch (order.Order)
                    {
                        case StorageOrder.Ascending:
                            q = q.OrderBy(order.OrderBy);
                            break;
                        case StorageOrder.Descending:
                            q = q.OrderByDescing(order.OrderBy);
                            break;
                    }
                }
            }
            // Limit
            if (query.Limit != null)
            {
                q = q.Top(query.Limit.Value);
            }

            var r = await q.ToListAsync();

            StorageResult<TEntity, TKey> result = new StorageResult<TEntity, TKey>();
            result.Result = r;

            return result;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>查询结果</returns>
        public async Task<StoragePageResult<TEntity, TKey>> PageAsync(StoragePageQuery<TEntity, TKey> query, IStorageContext context)
        {
            StorageContextBase? contextBase = context as StorageContextBase;
            if (contextBase == null)
                return await Task.FromResult(StoragePageResult<TEntity, TKey>.Fail("context is not StorageContextBase"));

            var comm = contextBase.MySqlConnection.QuerySet<TEntity>();
            // Where
            var q = comm.Where(query.Where);
            // Order
            if (query.OrderOptions != null && query.OrderOptions.Count > 0)
            {
                foreach (var order in query.OrderOptions)
                {
                    switch (order.Order)
                    {
                        case StorageOrder.Ascending:
                            q = q.OrderBy(order.OrderBy);
                            break;
                        case StorageOrder.Descending:
                            q = q.OrderByDescing(order.OrderBy);
                            break;
                    }
                }
            }

            var r = await q.PageListAsync(query.Page, query.PageSize);

            StoragePageResult<TEntity, TKey> result = new StoragePageResult<TEntity, TKey>();
            result.Result = r.Items;
            result.Page = r.PageIndex;
            result.PageSize = r.PageSize;
            result.PageCount = r.TotalPage;
            result.TotalCount = r.Total;

            return result;
        }
    }
}

