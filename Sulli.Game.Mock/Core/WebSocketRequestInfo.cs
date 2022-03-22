using Sulli.Game.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Mock
{
    /// <summary>
    /// WebSocket请求信息
    /// </summary>
    public class WebSocketRequestInfo
    {
        /// <summary>
        /// 状态
        /// </summary>
        public WebSocketRequestInfoStatus Status { get; set; }

        /// <summary>
        /// 请求数据
        /// </summary>
        public RequestData RequestData { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public ResponseData ResponseData { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// 返回时间
        /// </summary>
        public DateTime ResponseTime { get; set; }

    }
}
