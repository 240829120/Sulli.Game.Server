using Sulli.Game.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace Sulli.Game.Mock
{
    /// <summary>
    /// WebSocket客户端
    /// </summary>
    public class WebSocketClient
    {
        /// <summary>
        /// WebSocket客户端
        /// </summary>
        /// <param name="url">地址</param>
        public WebSocketClient(string url)
        {
            this.WebSocket = new WebSocket(url);
            this.WebSocket.OnOpen += WebSocket_OnOpen;
            this.WebSocket.OnClose += WebSocket_OnClose;
            this.WebSocket.OnError += WebSocket_OnError;
            this.WebSocket.OnMessage += WebSocket_OnMessage;
        }


        /// <summary>
        /// WebSocket连接
        /// </summary>
        public WebSocket WebSocket { get; private set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public TimeSpan TimeOut { get; set; } = TimeSpan.FromSeconds(15);

        /// <summary>
        /// 触发日志写入
        /// </summary>
        public Action<string> OnLog { get; set; }

        /// <summary>
        /// 请求字典
        /// </summary>
        private ConcurrentDictionary<string, WebSocketRequestInfo> RequestDic = new ConcurrentDictionary<string, WebSocketRequestInfo>();

        /// <summary>
        /// 调用
        /// </summary>
        /// <typeparam name="TResult">返回数据类型</typeparam>
        /// <param name="path">地址</param>
        /// <param name="data">请求数据</param>
        /// <param name="header">头部</param>
        /// <returns>调用结果</returns>
        public async Task<TResult> CallAsync<TResult>(string path, object data, Dictionary<string, string> header)
        {
            string json = await CallAsync(path, (data == null || (data is string s && string.IsNullOrWhiteSpace(s))) ? null : JsonConvert.SerializeObject(data), header);
            TResult result = JsonConvert.DeserializeObject<TResult>(json);

            return result;
        }

        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="path">地址</param>
        /// <param name="data">请求数据</param>
        /// <param name="header">头部</param>
        /// <returns>调用结果</returns>
        public async Task<string> CallAsync(string path, string data, Dictionary<string, string> header)
        {
            return await Task.Run(() =>
            {
                WebSocketRequestInfo info = new WebSocketRequestInfo();
                info.RequestTime = DateTime.Now;

                info.RequestData = new RequestData();
                info.RequestData.ID = Guid.NewGuid().ToString();
                info.RequestData.Body = data;
                info.RequestData.Header = header;
                info.RequestData.Uri = path;

                this.RequestDic.AddOrUpdate(info.RequestData.ID, info, (k, v) => info);

                string json = JsonConvert.SerializeObject(info.RequestData);
                byte[] buffer = Encoding.UTF8.GetBytes(json);

                this.WebSocket.Send(buffer);

                while ((DateTime.Now - info.RequestTime) < this.TimeOut)
                {
                    if (info.ResponseData != null)
                    {
                        break;
                    }

                    Task.Delay(500).Wait();
                }

                this.RequestDic.TryRemove(info.RequestData.ID, out var item);
                info.Status = info.ResponseData == null ? WebSocketRequestInfoStatus.Timeout : WebSocketRequestInfoStatus.None;

                return info?.ResponseData?.Body;
            });
        }

        /// <summary>
        /// 处理消息
        /// </summary>
        private void WebSocket_OnMessage(object sender, MessageEventArgs e)
        {
            string json = Encoding.UTF8.GetString(e.RawData);

            ResponseData response = null;
            response = JsonConvert.DeserializeObject<ResponseData>(json);

            if (response.Type != ResponseDataType.Empty.Type)
            {
                this.OnLog($"收到消息: {json}");
                return;
            }

            if (this.RequestDic.TryGetValue(response.ID, out var info))
            {
                info.ResponseData = response;
                return;
            }

            this.OnLog($"收到消息: {json}");
        }

        /// <summary>
        /// 建立连接
        /// </summary>
        private void WebSocket_OnOpen(object sender, EventArgs e)
        {
            this.OnLog?.Invoke($"建立连接");
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        private void WebSocket_OnClose(object sender, CloseEventArgs e)
        {
            this.OnLog?.Invoke($"关闭连接");
        }

        /// <summary>
        /// 异常
        /// </summary>
        private void WebSocket_OnError(object sender, WebSocketSharp.ErrorEventArgs e)
        {
            this.OnLog?.Invoke($"连接异常: e: {e.Message}");
        }
    }
}
