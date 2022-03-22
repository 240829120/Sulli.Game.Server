using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sulli.Game.Server.Connection.WebSocket
{
    /// <summary>
    /// Json返回处理
    /// </summary>
    public class JsonResultProvider : IWebSocketResultProvider
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
        /// <param name="behavior">连接行为</param>
        public void Execute(ServerEngine engine, RequestContext requestContext, ResponseContext responseContext, WebSocketConnectionBehavior behavior)
        {
            JsonResult result = responseContext.Result as JsonResult;

            string body = result.Value == null ? string.Empty : JsonConvert.SerializeObject(result.Value);

            ResponseData response = new ResponseData();
            response.ID = requestContext.ID;
            response.Body = body;

            string json = JsonConvert.SerializeObject(response);

            byte[] buffer = Encoding.UTF8.GetBytes(json);

            behavior.ExecuteSend(buffer);
        }
    }
}
