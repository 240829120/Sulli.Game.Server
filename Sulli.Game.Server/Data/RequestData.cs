using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 请求数据
    /// </summary>
    public class RequestData
    {
        /// <summary>
        /// 请求ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 请求URI
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// 头部
        /// </summary>
        public Dictionary<string, string> Header { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }
    }
}
