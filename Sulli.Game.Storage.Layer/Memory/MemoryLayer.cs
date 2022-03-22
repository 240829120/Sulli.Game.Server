using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Sulli.Game.Core;

namespace Sulli.Game.Storage.Layer
{
    /// <summary>
    /// 内存仓储层
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TKey">键</typeparam>
    public class MemoryLayer<TEntity, TKey> : StorageLayerBase, IStorageLayer<TEntity, TKey> where TEntity : EntityBase<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 内存仓储层
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="option">设置</param>
        public MemoryLayer(string name, MemoryLayerOption option) : base(name)
        {
            this.collection = new List<TEntity>(option.InitCapacity);
        }

        /// <summary>
        /// 集合
        /// </summary>
        private List<TEntity> collection;

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
            return await Task.Run(() =>
            {
                try
                {
                    LockHelper.Write(lock_object, () => this.collection.Add(entity));
                }
                catch (Exception ex)
                {
                    return StorageResult.Fail(ex);
                }

                return StorageResult.Success();
            });
        }

        /// <summary>
        /// 插入多个
        /// </summary>
        /// <param name="entitys">实体集合</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>仓储返回</returns>
        public async Task<StorageResult> InsertManyAsync(IEnumerable<TEntity> entitys, IStorageContext context)
        {
            return await Task.Run(() =>
            {
                try
                {
                    LockHelper.Write(lock_object, () => this.collection.AddRange(entitys));
                }
                catch (Exception ex)
                {
                    return StorageResult.Fail(ex);
                }

                return StorageResult.Success();
            });
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
            return await Task.FromResult(StorageResult.Success());
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
            return await Task.Run(() =>
            {
                try
                {
                    LockHelper.Write(lock_object, () =>
                    {
                        TEntity? value = this.collection.FirstOrDefault(p => p.ID.Equals(entity.ID));

                        if (value == null)
                        {
                            this.collection.Add(entity);
                        }
                    });
                }
                catch (Exception ex)
                {
                    StorageResult.Fail(ex);
                }

                return StorageResult.Success();
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>查询结果</returns>
        public async Task<StorageResult<TEntity, TKey>> FindAsync(StorageQuery<TEntity, TKey> query, IStorageContext context)
        {
            return await Task.Run(() =>
            {
                StorageResult<TEntity, TKey> result = new StorageResult<TEntity, TKey>();

                try
                {
                    IEnumerable<TEntity> find = this.collection;
                    if (query.Where != null)
                    {
                        find = find.Where(query.Where.Compile());
                    }
                    if (query.Limit != null)
                    {
                        find = find.Take(query.Limit.Value);
                    }

                    if (query.OrderOptions != null && query.OrderOptions.Count > 0)
                    {
                        foreach (var order in query.OrderOptions)
                        {
                            switch (order.Order)
                            {
                                case StorageOrder.Ascending:
                                    find = find.OrderBy(order.OrderBy.Compile());
                                    break;
                                case StorageOrder.Descending:
                                    find = find.OrderByDescending(order.OrderBy.Compile());
                                    break;
                            }
                        }
                    }
                    LockHelper.Read(lock_object, () =>
                    {
                        result.Result = find.ToList();
                    });
                }
                catch (Exception ex)
                {
                    return StorageResult<TEntity, TKey>.Fail(ex);
                }

                return result;
            });
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="context">仓储上下文</param>
        /// <returns>查询结果</returns>
        public async Task<StoragePageResult<TEntity, TKey>> PageAsync(StoragePageQuery<TEntity, TKey> query, IStorageContext context)
        {
            return await Task.Run(() =>
            {
                StoragePageResult<TEntity, TKey> result = new StoragePageResult<TEntity, TKey>();

                try
                {
                    result.Page = query.Page;
                    result.PageSize = query.PageSize;

                    IEnumerable<TEntity> find = this.collection;
                    if (query.Where != null)
                    {
                        find = find.Where(query.Where.Compile());
                    }
                    LockHelper.Read(lock_object, () =>
                    {
                        result.TotalCount = find.Count();

                        foreach (var order in query.OrderOptions)
                        {
                            switch (order.Order)
                            {
                                case StorageOrder.Ascending:
                                    find = find.OrderBy(order.OrderBy.Compile());
                                    break;
                                case StorageOrder.Descending:
                                    find = find.OrderByDescending(order.OrderBy.Compile());
                                    break;
                            }
                        }

                        result.Result = find.ToList();
                    });

                    result.PageCount = (int)Math.Ceiling((double)result.TotalCount / result.PageSize);
                }
                catch (Exception ex)
                {
                    return StoragePageResult<TEntity, TKey>.Fail(ex);
                }

                return result;
            });
        }
    }
}
