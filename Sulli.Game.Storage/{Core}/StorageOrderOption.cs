using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储排序设置
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TKey">键</typeparam>
    public class StorageOrderOption<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 仓储排序设置
        /// </summary>
        public StorageOrderOption()
        { }

        /// <summary>
        /// 仓储排序设置
        /// </summary>
        /// <param name="orderBy">排序字段</param>
        /// <param name="order">排序方式</param>
        public StorageOrderOption(Expression<Func<TEntity, object>> orderBy, StorageOrder order)
        {
            this.OrderBy = orderBy;
            this.Order = order;
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        public Expression<Func<TEntity, object>> OrderBy { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public StorageOrder Order { get; set; }
    }
}
