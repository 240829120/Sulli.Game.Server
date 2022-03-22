using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server.Connection.WebSocket
{
    /// <summary>
    /// WebSocket连接设置
    /// </summary>
    public class WebSocketConnectionOption
    {
        /// <summary>
        /// URI前缀
        /// </summary>
        public List<string> UriPrefixs { get; private set; } = new List<string>();

        /// <summary>
        /// 消息推送超时时间
        /// </summary>
        public TimeSpan MessagePushTimeout { get; set; } = TimeSpan.FromSeconds(5);
    }
}
