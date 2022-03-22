using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sulli.Game.Core;
using log4net;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储连接池
    /// </summary>
    public class StorageConnectionPool
    {
        /// <summary>
        /// 仓储连接池
        /// </summary>
        /// <param name="option">连接池设置</param>
        /// <param name="createConnection">创建连接方法</param>
        public StorageConnectionPool(StorageConnectionPoolOption option, Func<IStorageConnection> createConnection)
        {
            this.Option = option;
            this.CreateConnection = createConnection;

            this.BeginCheck();
        }

        /// <summary>
        /// 仓储连接池设置
        /// </summary>
        public StorageConnectionPoolOption Option { get; private set; }

        /// <summary>
        /// 创建连接
        /// </summary>
        public Func<IStorageConnection> CreateConnection { get; private set; }

        /// <summary>
        /// 日志
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(StorageConnectionInfo));

        /// <summary>
        /// 连接池信息结婚
        /// </summary>
        private List<StorageConnectionInfo> Infos = new List<StorageConnectionInfo>();

        /// <summary>
        /// 锁对象
        /// </summary>
        private ReaderWriterLockSlim lock_object = new ReaderWriterLockSlim();

        /// <summary>
        /// 获取一个连接
        /// </summary>
        /// <returns>数据库连接</returns>
        public IStorageConnection? GetConnection()
        {
            StorageConnectionInfo? info = null;

            for (int i = 0; i < this.Option.GetConnectionTryTimes; i++)
            {
                LockHelper.Write(this.lock_object, () =>
                {
                    info = this.Infos.FirstOrDefault(p => !p.IsUsing);

                    if (info == null && this.Infos.Count < this.Option.Max)
                    {
                        IStorageConnection? connection = this.CreateConnection();
                        if (connection == null)
                        {
                            return;
                        }

                        info = new StorageConnectionInfo(connection);

                        this.Infos.Add(info);
                    }

                    if (info == null)
                        return;

                    info.IsUsing = true;
                    info.UsingTime = DateTime.Now;
                    info.ReturnTime = null;

                }, this.Option.GetConnectionTimeOut);

                if (info != null)
                    break;

                if (i == this.Option.GetConnectionTryTimes - 1)
                    return null;

                Task.Delay(500).Wait();
            }

            return info?.Connection;
        }

        /// <summary>
        /// 开始检测
        /// </summary>
        private void BeginCheck()
        {
            if (Application.Current == null)
                return;

            Application.Current.BeginInvoke(ApplicationWorkMode.Forever, this.Option.ConnectionCheckInterval, () =>
            {
                Task.Run(() =>
                {
                    try
                    {
                        this.ExecuteCheck();
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                });
            });
        }

        /// <summary>
        /// 执行检测
        /// </summary>
        private void ExecuteCheck()
        {
            if (this.Infos.Count <= this.Option.Min)
                return;

            List<StorageConnectionInfo>? removeList = null;

            LockHelper.Write(this.lock_object, () =>
            {
                DateTime now = DateTimeHelper.Now;

                // 连接使用超时 || 连接长时间未使用
                removeList = this.Infos.Where(p => (p.IsUsing && (now - p.UsingTime) > this.Option.ConnectionUsingTimeOut) || (!p.IsUsing && (now - p.ReturnTime) > this.Option.ConnectionFreeTimeToRemove)).ToList();
                foreach (StorageConnectionInfo item in removeList)
                {
                    this.Infos.Remove(item);
                }
            });

            if (removeList == null)
                return;

            DateTime now = DateTimeHelper.Now;

            foreach (StorageConnectionInfo info in removeList)
            {
                info.Dispose();
            }
        }
    }
}
