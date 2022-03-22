using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 返回上下文
    /// </summary>
    public class ResponseContext
    {
        /// <summary>
        /// 返回值
        /// </summary>
        public IResult Result { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; set; }
    }
}
