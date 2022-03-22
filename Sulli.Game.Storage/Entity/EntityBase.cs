using Sulli.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey">键</typeparam>
    public abstract class EntityBase<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// ID
        /// </summary>
        public TKey ID { get; set; }
    }
}
