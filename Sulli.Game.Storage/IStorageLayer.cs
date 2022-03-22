using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储层
    /// </summary>
    public interface IStorageLayer
    {

    }

    /// <summary>
    /// 仓储层
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TKey">键</typeparam>
    public interface IStorageLayer<TEntity, TKey> : IStorageLayer, IStorage<TEntity, TKey>
        where TEntity : EntityBase<TKey>
        where TKey : IEquatable<TKey>
    {
    }
}
