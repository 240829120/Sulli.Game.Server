using Sulli.Game.Storage;
using Sulli.Game.Storage.Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Mock
{
    /// <summary>
    /// 测试仓储上下文
    /// </summary>
    public class MockStorageContext : StorageContextBase
    {
        public MockStorageContext()
        {
            StorageConnectionPool mysqlPool = StorageManager.GetConnectionPool("mysql");

            var connection = mysqlPool.GetConnection();
            if (connection == null)
                throw new Exception();

            this.MySqlConnection = connection.GetConnection<MySql.Data.MySqlClient.MySqlConnection>();
        }
    }
}
