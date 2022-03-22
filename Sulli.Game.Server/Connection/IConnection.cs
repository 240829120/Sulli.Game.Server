using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 连接器
    /// </summary>
    public interface IConnection : IDisposable
    {
        /// <summary>
        /// 服务引擎
        /// </summary>
        ServerEngine ServerEngine { get; }

        /// <summary>
        /// 启动监听
        /// </summary>
        void Start();
    }
}
