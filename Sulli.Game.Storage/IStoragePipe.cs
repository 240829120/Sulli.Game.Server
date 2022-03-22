using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储管道
    /// </summary>
    public interface IStoragePipe
    {

    }

    /// <summary>
    /// 数据管道
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TKey">键</typeparam>
    public interface IStoragePipe<TEntity, TKey> : IStoragePipe, IStorageLayer<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context">仓储上下文</param>
        Task InitializationAsync(IStorageContext context);
    }
}
