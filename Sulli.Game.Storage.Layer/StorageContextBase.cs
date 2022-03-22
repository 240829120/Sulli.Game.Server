using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage.Layer
{
    /// <summary>
    /// 仓储上下文
    /// </summary>
    public abstract class StorageContextBase : IStorageContext
    {
        /// <summary>
        /// MySql数据库连接
        /// </summary>
        public MySql.Data.MySqlClient.MySqlConnection? MySqlConnection { get; protected set; }
    }
}
