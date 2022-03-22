using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server.Connection.WebSocket
{
    /// <summary>
    /// WebSocket 连接事件参数
    /// </summary>
    public class WebSocketConnectionEventArgs : EventArgs
    {
        /// <summary>
        /// 连接ID
        /// </summary>
        public string? WebSocketID { get; set; }
    }
}
