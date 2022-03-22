using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Sulli.Game.Server.Connection.WebSocket
{
    /// <summary>
    /// WebSocket链接
    /// </summary>
    public class WebSocketConnection : IConnection, IDisposable
    {
        /// <summary>
        /// 头部会话
        /// </summary>
        public const string DATA__SESSION = "DATA__SESSION";

        /// <summary>
        /// WebSocket链接
        /// </summary>
        /// <param name="serverEngine">服务引擎</param>
        /// <param name="option">设置</param>
        /// <param name="connectionManager">连接管理器</param>
        public WebSocketConnection(ServerEngine serverEngine, WebSocketConnectionOption option)
        {
            this.ServerEngine = serverEngine;
            this.Option = option;

            this.resultProviders.Add(new JsonResultProvider());
        }

        /// <summary>
        /// 服务引擎
        /// </summary>
        public ServerEngine ServerEngine { get; private set; }

        /// <summary>
        /// 设置
        /// </summary>
        public WebSocketConnectionOption Option { get; private set; }

        /// <summary>
        /// WebSocket链接
        /// </summary>
        public List<WebSocketServer> WebSocketServers { get; private set; } = new List<WebSocketServer>();

        /// <summary>
        /// WebSocket链接行为集合
        /// </summary>
        public ConcurrentDictionary<string, WebSocketConnectionBehavior> WebSocketConnectionBehaviors { get; private set; } = new ConcurrentDictionary<string, WebSocketConnectionBehavior>();

        /// <summary>
        /// WebSocket返回值处理器
        /// </summary>
        private List<IWebSocketResultProvider> resultProviders = new List<IWebSocketResultProvider>();

        /// <summary>
        /// 连接时触发
        /// </summary>
        public event EventHandler<WebSocketConnectionEventArgs> OnConnect;

        /// <summary>
        /// 断开连接时触发
        /// </summary>
        public event EventHandler<WebSocketConnectionEventArgs> OnDisconnect;

        /// <summary>
        /// 发生错误时触发
        /// </summary>
        public event EventHandler<WebSocketSharp.ErrorEventArgs> OnError;

        /// <summary>
        /// 启动监听
        /// </summary>
        public void Start()
        {
            foreach (string uriPrefix in this.Option.UriPrefixs)
            {
                WebSocketServer webSocketServer = new WebSocketServer(uriPrefix);
                webSocketServer.AddWebSocketService<WebSocketConnectionBehavior>("/", behavior =>
                {
                    behavior.ServerEngine = this.ServerEngine;
                    behavior.Connection = this;
                    behavior.ResultProviders = this.resultProviders;
                });

                this.WebSocketServers.Add(webSocketServer);

                webSocketServer.Start();
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            foreach (WebSocketServer server in this.WebSocketServers)
            {
                server.Stop();
            }
        }

        /// <summary>
        /// 触发建立连接事件
        /// </summary>
        /// <param name="websocketId">连接ID</param>
        internal void ExecuteOnConnect(string websocketId)
        {
            if (this.OnConnect == null)
                return;

            WebSocketConnectionEventArgs args = new WebSocketConnectionEventArgs();
            args.WebSocketID = websocketId;

            this.OnConnect.Invoke(this, args);
        }

        /// <summary>
        /// 触发断开连接事件
        /// </summary>
        /// <param name="websocketId">连接ID</param>
        internal void ExecuteOnDisconnect(string websocketId)
        {
            if (this.OnDisconnect == null)
                return;

            WebSocketConnectionEventArgs args = new WebSocketConnectionEventArgs();
            args.WebSocketID = websocketId;

            this.OnDisconnect.Invoke(this, args);
        }

        /// <summary>
        /// 触发异常事件
        /// </summary>
        /// <param name="error">异常</param>
        internal void ExecuteOnError(WebSocketSharp.ErrorEventArgs error)
        {
            if (this.OnError == null)
                return;

            this.OnError.Invoke(this, error);
        }
    }
}
