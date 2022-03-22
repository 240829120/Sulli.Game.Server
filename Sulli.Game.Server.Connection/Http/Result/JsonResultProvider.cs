using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sulli.Game.Server.Connection.Http
{
    /// <summary>
    /// Json返回处理
    /// </summary>
    public class JsonResultProvider : IHttpResultProvider
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
            return responseContext.Result is JsonResult;
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
            JsonResult result = responseContext.Result as JsonResult;

            HttpListenerResponse response = httpListenerContext.Response;
            response.StatusCode = (int)HttpStatusCode.OK;
            response.ContentType = "application/json;charset=UTF-8";
            response.ContentEncoding = Encoding.UTF8;
            response.AppendHeader("Content-Type", "application/json;charset=UTF-8");

            string result_str = JsonConvert.SerializeObject(result.Value);

            using (StreamWriter writer = new StreamWriter(response.OutputStream, Encoding.UTF8))
            {
                await writer.WriteAsync(result_str);
                writer.Close();
                response.Close();
            }
        }
    }
}
