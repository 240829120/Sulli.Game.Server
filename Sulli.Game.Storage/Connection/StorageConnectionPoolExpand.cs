using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储连接池扩展
    /// </summary>
    public static class StorageConnectionPoolExpand
    {
        /// <summary>
        /// 获取连接
        /// </summary>
        /// <typeparam name="T">连接类型</typeparam>
        /// <param name="pool">连接池</param>
        /// <returns>连接</returns>
        public static T? GetConnection<T>(this StorageConnectionPool pool) where T : class, IStorageConnection
        {
            IStorageConnection? connection = pool.GetConnection();
            return connection as T;
        }
    }
}
