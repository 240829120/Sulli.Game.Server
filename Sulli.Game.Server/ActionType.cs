using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 方法类型
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// 未指定
        /// </summary>
        None,

        /// <summary>
        /// WebApi
        /// </summary>
        WebApi,

        /// <summary>
        /// WebSocket
        /// </summary>
        WebSocket,

        /// <summary>
        /// Tcp
        /// </summary>
        Tcp
    }
}
