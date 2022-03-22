using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace Sulli.Game.Server.Connection.Http
{
    /// <summary>
    /// 文件返回处理
    /// </summary>
    public class FileResultProvider : IHttpResultProvider
    {
        /// <summary>
        /// 是否可以执行
        /// </summary>
        /// <param name="engine">引擎</param>
        /// <param name="requestContext">请求上下文</param>
        /// <param name="responseContext">返回上下文</param>
        /// <returns>是否可以执行</returns>
        public bool CanExecute(ServerEngine engine, RequestContext requestContext, ResponseContext responseContext)
        {
            return responseContext.Result is FileResult;
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
            FileResult result = responseContext.Result as FileResult;

            HttpListenerResponse response = httpListenerContext.Response;
            response.StatusCode = (int)HttpStatusCode.OK;
            response.ContentType = "application/octet-stream";
            response.AppendHeader("Content-Type", "application/octet-stream");
            response.AppendHeader("Content-Disposition", $"attachment;FileName={HttpUtility.UrlEncode(result.FileName)}");

            using (FileStream fs = new FileStream(result.FullPath, FileMode.Open, FileAccess.Read))
            {
                response.ContentLength64 = fs.Length;

                byte[] buffer = new byte[10240];
                int offset = 0;
                int read = 0;

                while ((read = await fs.ReadAsync(buffer, offset, buffer.Length)) > 0)
                {
                    await response.OutputStream.WriteAsync(buffer, 0, read);
                    offset += read;
                }

                fs.Close();
                await fs.DisposeAsync();
                response.OutputStream.Close();
                await response.OutputStream.DisposeAsync();
            }
        }
    }
}
