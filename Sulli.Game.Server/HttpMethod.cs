using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// Http请求方法
    /// </summary>
    [Flags]
    public enum HttpMethod
    {
        /// <summary>
        /// Get请求
        /// </summary>
        GET,
        /// <summary>
        /// Post请求
        /// </summary>
        POST
    }
}
