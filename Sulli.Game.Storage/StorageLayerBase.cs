using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储层基类
    /// </summary>
    public abstract class StorageLayerBase : IStorageLayer
    {
        /// <summary>
        /// 仓储层名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 仓储层基类
        /// </summary>
        /// <param name="name">名称</param>
        public StorageLayerBase(string name)
        {
            this.Name = name;
        }
    }
}
