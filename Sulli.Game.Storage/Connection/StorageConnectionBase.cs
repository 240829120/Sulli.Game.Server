using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储连接基类
    /// </summary>
    public abstract class StorageConnectionBase : IStorageConnection
    {
        /// <summary>
        /// 仓储连接基类
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public StorageConnectionBase(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// 销毁时触发
        /// </summary>
        public event EventHandler<EventArgs>? OnDispose;

        /// <summary>
        /// 建立连接
        /// </summary>
        public abstract void Open();

        /// <summary>
        /// 销毁连接
        /// </summary>
        public abstract void DestoryConnection();

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <typeparam name="T">连接类型</typeparam>
        /// <returns>连接</returns>
        public abstract T? GetConnection<T>() where T : class;

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            this.OnDispose?.Invoke(this, EventArgs.Empty);
        }
    }
}
