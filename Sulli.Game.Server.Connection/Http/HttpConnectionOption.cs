using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server.Connection.Http
{
    /// <summary>
    /// HTTP链接设置
    /// </summary>
    public class HttpConnectionOption
    {
        /// <summary>
        /// URI前缀
        /// </summary>
        public List<string> UriPrefixs { get; private set; } = new List<string>();
    }
}
