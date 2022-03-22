using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 请求上下文
    /// </summary>
    public class RequestContext
    {
        /// <summary>
        /// 请求上下文
        /// </summary>
        /// <param name="uri">请求地址</param>
        /// <param name="body">请求内容</param>
        public RequestContext(Uri uri, string body)
        {
            this.Uri = uri;
            this.Body = body;
        }

        /// <summary>
        /// 请求ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 请求头部
        /// </summary>
        public Dictionary<string, object> Header { get; private set; } = new Dictionary<string, object>();

        /// <summary>
        /// 请求参数
        /// </summary>
        public Uri Uri { get; private set; }

        /// <summary>
        /// 内容体
        /// </summary>
        public string Body { get; private set; }
    }
}
