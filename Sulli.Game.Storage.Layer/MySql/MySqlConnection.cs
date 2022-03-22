using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sulli.Game.Core;
using Kogel.Dapper.Extension.MySql;

namespace Sulli.Game.Storage.Layer
{
    /// <summary>
    /// MySql仓储连接
    /// </summary>
    public class MySqlConnection : StorageConnectionBase
    {
        /// <summary>
        /// 仓储连接基类
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public MySqlConnection(string connectionString) : base(connectionString)
        {
            this.Connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
        }

        /// <summary>
        /// 连接
        /// </summary>
        public MySql.Data.MySqlClient.MySqlConnection Connection { get; private set; }

        /// <summary>
        /// 建立连接
        /// </summary>
        public override void Open()
        {
            this.Connection?.Open();
        }

        /// <summary>
        /// 销毁连接
        /// </summary>
        public override void DestoryConnection()
        {
            this.Connection?.Dispose();
        }

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <typeparam name="T">连接类型</typeparam>
        /// <returns>连接</returns>
        public override T? GetConnection<T>() where T : class
        {
            return this.Connection as T;
        }
    }
}
