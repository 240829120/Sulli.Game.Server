using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 服务异常事件参数
    /// </summary>
    public class ServerExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// 服务引擎
        /// </summary>
        public ServerEngine Engine { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        public ServerException Exception { get; set; }
    }
}
