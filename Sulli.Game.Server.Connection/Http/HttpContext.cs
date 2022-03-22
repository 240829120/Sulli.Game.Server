using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server.Connection.Http
{
    /// <summary>
    /// Http上下文
    /// </summary>
    public class HttpContext
    {
        /// <summary>
        /// HTTP监听器
        /// </summary>
        public HttpListener HttpListener { get; set; }
    }
}
