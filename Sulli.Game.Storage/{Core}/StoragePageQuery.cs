using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储分页查询
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class StoragePageQuery<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 条件
        /// </summary>
        public Expression<Func<TEntity, bool>> Where { get; set; }

        /// <summary>
        /// 排序条件
        /// </summary>
        public List<StorageOrderOption<TEntity, TKey>> OrderOptions { get; set; } = new List<StorageOrderOption<TEntity, TKey>>();

        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 空
        /// </summary>
        public static StoragePageQuery<TEntity, TKey> None { get; } = new StoragePageQuery<TEntity, TKey>();
    }
}
