using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储管理器
    /// </summary>
    public static class StorageManager
    {
        /// <summary>
        /// 连接池
        /// </summary>
        private static Dictionary<string, StorageConnectionPool> connectionPools = new Dictionary<string, StorageConnectionPool>();

        /// <summary>
        /// 仓储管道池
        /// </summary>
        private static Dictionary<string, IStoragePipe> storagePipes = new Dictionary<string, IStoragePipe>();

        /// <summary>
        /// 添加连接池
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="pool">连接池</param>
        public static void AddConnectionPool(string name, StorageConnectionPool pool)
        {
            lock (connectionPools)
            {
                connectionPools.Add(name, pool);
            }
        }

        /// <summary>
        /// 获取连接池
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>连接池</returns>
        public static StorageConnectionPool GetConnectionPool(string name)
        {
            return connectionPools[name];
        }

        /// <summary>
        /// 添加仓储管道
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="pipe">仓储管道</param>
        public static void AddStoragePipe(string name, IStoragePipe pipe)
        {
            lock (storagePipes)
            {
                storagePipes.Add(name, pipe);
            }
        }

        /// <summary>
        /// 获取仓储管道
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>仓储管道</returns>
        public static IStoragePipe GetStoragePipe(string name)
        {
            return storagePipes[name];
        }
    }
}
