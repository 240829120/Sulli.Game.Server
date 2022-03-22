using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server.Connection.WebSocket
{
    /// <summary>
    /// WebSocket返回处理类
    /// </summary>
    public interface IWebSocketResultProvider
    {
        /// <summary>
        /// 是否可以执行
        /// </summary>
        /// <param name="engine">引擎</param>
        /// <param name="requestContext">请求上下文</param>
        /// <param name="responseContext">返回上下文</param>
        /// <returns>是否可以执行</returns>
        bool CanExecute(ServerEngine engine, RequestContext requestContext, ResponseContext responseContext);

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="engine">引擎</param>
        /// <param name="requestContext">请求上下文</param>
        /// <param name="responseContext">返回上下文</param>
        /// <param name="behavior">连接行为</param>
        void Execute(ServerEngine engine, RequestContext requestContext, ResponseContext responseContext, WebSocketConnectionBehavior behavior);
    }
}
