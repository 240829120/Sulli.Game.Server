using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sulli.Game.Server.Connection.Test
{
    /// <summary>
    /// 测试链接
    /// </summary>
    public class TestConnection : IConnection, IDisposable
    {
        /// <summary>
        /// 服务引擎
        /// </summary>
        public ServerEngine ServerEngine { get; private set; }

        /// <summary>
        /// 测试链接
        /// </summary>
        /// <param name="serverEngine">服务引擎</param>
        public TestConnection(ServerEngine serverEngine)
        {
            this.ServerEngine = serverEngine;
        }

        /// <summary>
        /// 异步调用
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="uri">地址</param>
        /// <param name="requestData">请求数据</param>
        /// <param name="header">请求头</param>
        /// <returns>调用结果</returns>
        public async Task<T> CallAsync<T>(Uri uri, object requestData, Dictionary<string, string> header = null)
        {
            string body = requestData == null ? string.Empty : JsonConvert.SerializeObject(requestData);

            RequestContext requestContext = new RequestContext(uri, body);
            if (header != null)
            {
                foreach (var kv in header)
                {
                    requestContext.Header.Add(kv.Key, kv.Value);
                }
            }

            ResponseContext responseContext = new ResponseContext();

            await this.ServerEngine.CallAsync(requestContext, responseContext);

            if (responseContext.Result is not JsonResult result || result.Value == null)
                return default;

            string result_json = JsonConvert.SerializeObject(result.Value);

            return JsonConvert.DeserializeObject<T>(result_json);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {

        }
    }
}
