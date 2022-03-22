using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储初始化异常
    /// </summary>
    public class StorageInitializationException : Exception
    {
        /// <summary>
        /// 仓储初始化异常
        /// </summary>
        /// <param name="layer">层</param>
        /// <param name="message">消息</param>
        /// <param name="innerException">内部异常</param>
        public StorageInitializationException(IStorageLayer layer, string? message, Exception? innerException) : base(message, innerException)
        {
            this.Layer = layer;
        }

        /// <summary>
        /// 仓储初始化异常
        /// </summary>
        /// <param name="layer">层</param>
        public StorageInitializationException(IStorageLayer layer) : this(layer, null, null)
        {

        }

        /// <summary>
        /// 仓储层
        /// </summary>
        public IStorageLayer Layer { get; }
    }
}
