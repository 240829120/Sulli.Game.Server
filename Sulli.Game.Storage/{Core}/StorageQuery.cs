using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储查询
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class StorageQuery<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 条件
        /// </summary>
        public Expression<Func<TEntity, bool>>? Where { get; set; }

        /// <summary>
        /// 排序条件
        /// </summary>
        public List<StorageOrderOption<TEntity, TKey>> OrderOptions { get; set; } = new List<StorageOrderOption<TEntity, TKey>>();

        /// <summary>
        /// 获取
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// 空
        /// </summary>
        public static StorageQuery<TEntity, TKey> None { get; } = new StorageQuery<TEntity, TKey>();
    }
}
