using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;
using log4net;

namespace Sulli.Game.Server.Connection.WebSocket
{
    /// <summary>
    /// WebSocket连接行为处理器
    /// </summary>
    public class WebSocketConnectionBehavior : WebSocketBehavior
    {
        /// <summary>
        /// 日志
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(WebSocketConnectionBehavior));

        /// <summary>
        /// 服务器ID
        /// </summary>
        public int ServerID { get; set; }

        /// <summary>
        /// 玩家ID
        /// </summary>
        public string PlayerID { get; set; }

        /// <summary>
        /// 服务引擎
        /// </summary>
        public ServerEngine ServerEngine { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public WebSocketConnection Connection { get; set; }

        /// <summary>
        /// 返回值处理器
        /// </summary>
        public List<IWebSocketResultProvider> ResultProviders { get; set; }

        /// <summary>
        /// 执行发送
        /// </summary>
        /// <param name="data">数据</param>
        public void ExecuteSend(byte[] data)
        {
            this.Send(data);
        }

        /// <summary>
        /// 异步执行发送
        /// </summary>
        /// <param name="data">数据</param>
        public async Task ExecuteSendAsync(byte[] data)
        {
            await Task.Run(() =>
            {
                this.Send(data);
            });
        }

        /// <summary>
        /// 打开时
        /// </summary>
        protected override void OnOpen()
        {
            base.OnOpen();

            this.Connection.WebSocketConnectionBehaviors.AddOrUpdate(this.ID, this, (s, b) => this);

            this.Connection.ExecuteOnConnect(this.ID);
        }

        /// <summary>
        /// 关闭时
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);

            this.Connection.WebSocketConnectionBehaviors.TryRemove(this.ID, out WebSocketConnectionBehavior behavior);

            this.Connection.ExecuteOnDisconnect(this.ID);
        }

        /// <summary>
        /// 错误时
        /// </summary>
        /// <param name="e">异常</param>
        protected override void OnError(WebSocketSharp.ErrorEventArgs e)
        {
            base.OnError(e);

            this.Connection.ExecuteOnError(e);
        }

        /// <summary>
        /// 接收到消息时触发
        /// </summary>
        /// <param name="e">消息</param>
        protected override void OnMessage(MessageEventArgs e)
        {
            base.OnMessage(e);

            Task.Run(async () =>
            {
                try
                {
                    await this.ExecuteMessage(e);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
        }

        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="e">消息</param>
        private async Task ExecuteMessage(MessageEventArgs e)
        {
            string request_json = null;
            if (e.IsText)
            {
                request_json = e.Data;
            }
            else if (e.IsBinary)
            {
                request_json = Encoding.UTF8.GetString(e.RawData);
            }

            RequestData request_data = JsonConvert.DeserializeObject<RequestData>(request_json);

            RequestContext requestContext = new RequestContext(new Uri(request_data.Uri), request_data.Body);
            requestContext.ID = request_data.ID;
            requestContext.Header.Add(WebSocketConnectionConstant.HEADER__WEB_SOCKET_ID, this.ID);
            if (request_data.Header != null && request_data.Header.Count > 0)
            {
                foreach (var kv in request_data.Header)
                {
                    requestContext.Header.Add(kv.Key, kv.Value);
                }
            }

            ResponseContext responseContext = new ResponseContext();

            await this.ServerEngine.CallAsync(requestContext, responseContext);

            foreach (IWebSocketResultProvider provider in this.ResultProviders)
            {
                if (!provider.CanExecute(this.ServerEngine, requestContext, responseContext))
                    continue;

                provider.Execute(this.ServerEngine, requestContext, responseContext, this);

                break;
            }
        }
    }
}
