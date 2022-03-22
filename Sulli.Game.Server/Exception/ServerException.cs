using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 服务异常
    /// </summary>
    public class ServerException : Exception
    {
        /// <summary>
        /// 服务异常
        /// </summary>
        /// <param name="message">消息</param>
        public ServerException(string message) : base(message)
        { }

        /// <summary>
        /// 服务异常
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="innerException">异常信息</param>
        public ServerException(string message, Exception innerException) : base(message, innerException)
        { }

        /// <summary>
        /// URI
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// 服务引擎
        /// </summary>
        public ServerEngine Engine { get; set; }

        /// <summary>
        /// 路由节点
        /// </summary>
        public RouteNode RouteNode { get; set; }

        /// <summary>
        /// 异常码
        /// </summary>
        public ServerExceptionCode Code { get; set; }

        /// <summary>
        /// 请求上下文
        /// </summary>
        public RequestContext RequestContext { get; set; }

        /// <summary>
        /// 返回上下文
        /// </summary>
        public ResponseContext ResponseContext { get; set; }

        /// <summary>
        /// 拦截器上下文
        /// </summary>
        public FilterContext FilterContext { get; set; }

        /// <summary>
        /// 异常阶段
        /// </summary>
        public ServerExceptionStage Stage { get; set; }
    }
}
