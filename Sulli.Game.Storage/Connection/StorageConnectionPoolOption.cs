using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Storage
{
    /// <summary>
    /// 仓储连接池设置
    /// </summary>
    public class StorageConnectionPoolOption
    {
        /// <summary>
        /// 最小连接数
        /// </summary>
        public int Min { get; set; } = 10;

        /// <summary>
        /// 最大连接数
        /// </summary>
        public int Max { get; set; } = 50;

        /// <summary>
        /// 获取连接超时时间
        /// </summary>
        public TimeSpan GetConnectionTimeOut { get; set; } = TimeSpan.FromSeconds(1);

        /// <summary>
        /// 获取连接尝试次数
        /// </summary>
        public int GetConnectionTryTimes { get; set; } = 3;

        /// <summary>
        /// 连接检测时间
        /// </summary>
        public TimeSpan ConnectionCheckInterval { get; set; } = TimeSpan.FromMinutes(5);

        /// <summary>
        /// 空闲连接超过该时间会被清理
        /// </summary>
        public TimeSpan ConnectionFreeTimeToRemove { get; set; } = TimeSpan.FromMinutes(5);

        /// <summary>
        /// 连接使用超时时间
        /// </summary>
        public TimeSpan ConnectionUsingTimeOut { get; set; } = TimeSpan.FromSeconds(30);
    }
}
