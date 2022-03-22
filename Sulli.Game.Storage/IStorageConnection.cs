using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储连接
    /// </summary>
    public interface IStorageConnection : IDisposable
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// 销毁时触发
        /// </summary>
        event EventHandler<EventArgs>? OnDispose;

        /// <summary>
        /// 建立连接
        /// </summary>
        void Open();

        /// <summary>
        /// 销毁连接
        /// </summary>
        void DestoryConnection();

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <typeparam name="T">连接类型</typeparam>
        /// <returns>连接</returns>
        public T? GetConnection<T>() where T : class;
    }
}
