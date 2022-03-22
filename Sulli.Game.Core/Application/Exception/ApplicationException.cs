using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 应用程序错误
    /// </summary>
    public class ApplicationException : Exception
    {
        /// <summary>
        /// 应用程序错误
        /// </summary>
        /// <param name="message">消息</param>
        public ApplicationException(string message) : base(message)
        { }
    }
}
