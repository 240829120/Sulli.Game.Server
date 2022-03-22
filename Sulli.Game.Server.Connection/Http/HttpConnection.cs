using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sulli.Game.Server;
using log4net;

namespace Sulli.Game.Server.Connection.Http
{
    /// <summary>
    /// HTTP链接
    /// </summary>
    public class HttpConnection : IConnection, IDisposable
    {
        /// <summary>
        /// 日志
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(HttpConnection));

        /// <summary>
        /// HTTP链接
        /// </summary>
        /// <param name="serverEngine">服务引擎</param>
        /// <param name="option">设置</param>
        public HttpConnection(ServerEngine serverEngine, HttpConnectionOption option)
        {
            this.ServerEngine = serverEngine;
            this.Option = option;

            this.resultProviders.Add(new JsonResultProvider());
            this.resultProviders.Add(new FileResultProvider());
            this.resultProviders.Add(new FileBufferResultProvider());
        }

        /// <summary>
        /// 服务引擎
        /// </summary>
        public ServerEngine ServerEngine { get; private set; }

        /// <summary>
        /// 设置
        /// </summary>
        public HttpConnectionOption Option { get; private set; }

        /// <summary>
        /// Http监听
        /// </summary>
        private HttpListener httpListener;

        /// <summary>
        /// 返回值处理器
        /// </summary>
        private List<IHttpResultProvider> resultProviders = new List<IHttpResultProvider>();

        /// <summary>
        /// 启动监听
        /// </summary>
        public void Start()
        {
            this.httpListener = new HttpListener();
            this.httpListener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            foreach (string uriPrefix in this.Option.UriPrefixs)
            {
                this.httpListener.Prefixes.Add(uriPrefix);
            }

            this.httpListener.Start();

            HttpContext context = new HttpContext();
            context.HttpListener = this.httpListener;

            this.httpListener.BeginGetContext(this.GetContextCallBack, context);
        }

        /// <summary>
        /// 获取上下文回调
        /// </summary>
        /// <param name="result">回调返回</param>
        private void GetContextCallBack(IAsyncResult result)
        {
            try
            {
                if (!this.httpListener.IsListening)
                    return;

                HttpListenerContext httpListenerContext = this.httpListener.EndGetContext(result);
                this.httpListener.BeginGetContext(this.GetContextCallBack, result.AsyncState);

                Task.Run(() =>
                {
                    this.ExecuteRequest(httpListenerContext);
                });
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 执行请求
        /// </summary>
        /// <param name="httpListenerContext">HTTP监听上下文</param>
        private async void ExecuteRequest(HttpListenerContext httpListenerContext)
        {
            try
            {
                StreamReader reader = new StreamReader(httpListenerContext.Request.InputStream);
                string body = await reader.ReadToEndAsync();
                RequestContext requestContext = new RequestContext(httpListenerContext.Request.Url, body);
                requestContext.Method = httpListenerContext.Request.HttpMethod.ToUpper();
                ResponseContext responseContext = new ResponseContext();

                // 添加Header
                foreach (string key in httpListenerContext.Request.Headers.AllKeys)
                {
                    requestContext.Header.Add(key, httpListenerContext.Request.Headers[key]);
                }

                // 调用
                await this.ServerEngine.CallAsync(requestContext, responseContext);

                // 处理结果
                foreach (IHttpResultProvider provider in this.resultProviders)
                {
                    if (!provider.CanExecute(this.ServerEngine, requestContext, responseContext))
                        continue;

                    provider.Execute(this.ServerEngine, requestContext, responseContext, httpListenerContext);
                    break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            this.httpListener?.Stop();
            this.httpListener?.Close();
        }
    }
}
