using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sulli.Game.Storage;
using Sulli.Game.Storage.Layer;
using Sulli.Game.Mock;

namespace Sulli.Game.UnitTest.Storage
{
    /// <summary>
    /// 仓储测试基类
    /// </summary>
    public abstract class StorageTestBase
    {
        [TestInitialize]
        public void Init()
        {
            // 连接池
            StorageConnectionPoolOption option = new StorageConnectionPoolOption();
            StorageConnectionPool mysqlConnPool = new StorageConnectionPool(option, () => new MySqlConnection("Server=192.168.252.128;Database=test;Uid=root;Pwd=my-secret-pw;"));

            StorageManager.AddConnectionPool("mysql", mysqlConnPool);

            // 数据管道
            StudentPipe studentPipe = new StudentPipe();
            StorageManager.AddStoragePipe("Student", studentPipe);
        }
    }
}
