using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using log4net;

namespace Sulli.Game.Server.Connection.Http
{
    /// <summary>
    /// 文件Buffer数据返回
    /// </summary>
    public class FileBufferResultProvider : IHttpResultProvider
    {
        /// <summary>
        /// 日志
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(FileBufferResultProvider));

        /// <summary>
        /// 是否可以执行
        /// </summary>
        /// <param name="engine">引擎</param>
        /// <param name="requestContext">请求上下文</param>
        /// <param name="responseContext">返回上下文</param>
        /// <returns>是否可以执行</returns>
        public bool CanExecute(ServerEngine engine, RequestContext requestContext, ResponseContext responseContext)
        {
            return responseContext.Result is FileBufferResult;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="engine">引擎</param>
        /// <param name="requestContext">请求上下文</param>
        /// <param name="responseContext">返回上下文</param>
        /// <param name="httpListenerContext">HTTP监听上下文</param>
        public async void Execute(ServerEngine engine, RequestContext requestContext, ResponseContext responseContext, HttpListenerContext httpListenerContext)
        {
            FileBufferResult? result = responseContext.Result as FileBufferResult;

            if (result == null)
            {
                log.Error("responseContext.Result is null.");

                return;
            }

            HttpListenerResponse response = httpListenerContext.Response;
            response.StatusCode = (int)HttpStatusCode.OK;
            response.ContentType = "application/octet-stream";
            response.AppendHeader("Content-Type", "application/octet-stream");
            response.AppendHeader("Content-Disposition", $"attachment;FileName={HttpUtility.UrlEncode(result.FileName)}");

            await response.OutputStream.WriteAsync(result.Buffer);
            response.OutputStream.Close();
            await response.OutputStream.DisposeAsync();
        }
    }
}
