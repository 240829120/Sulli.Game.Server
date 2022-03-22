using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Sulli.Game.Core;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储连接池信息
    /// </summary>
    internal class StorageConnectionInfo : IDisposable
    {
        /// <summary>
        /// 日志
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(StorageConnectionInfo));

        /// <summary>
        /// 仓储连接池信息
        /// </summary>
        /// <param name="connection">连接</param>
        public StorageConnectionInfo(IStorageConnection connection)
        {
            this.Connection = connection;
            this.Connection.OnDispose -= Connection_OnDispose;
            this.Connection.OnDispose += Connection_OnDispose;
        }

        /// <summary>
        /// 仓储连接
        /// </summary>
        public IStorageConnection Connection { get; private set; }

        /// <summary>
        /// 是否正在使用
        /// </summary>
        public bool IsUsing { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime? UsingTime { get; set; }

        /// <summary>
        /// 归还时间
        /// </summary>
        public DateTime? ReturnTime { get; set; }

        /// <summary>
        /// 连接销毁时触发
        /// </summary>
        private void Connection_OnDispose(object? sender, EventArgs e)
        {
            this.UsingTime = null;
            this.ReturnTime = DateTimeHelper.Now;
            this.IsUsing = false;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            this.UsingTime = null;
            this.ReturnTime = DateTimeHelper.Now;
            this.IsUsing = false;

            this.Connection.DestoryConnection();
        }
    }
}
