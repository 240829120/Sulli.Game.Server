using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Message
{
    /// <summary>
    /// 消息发送返回值
    /// </summary>
    public class MessageSendResult
    {
        /// <summary>
        /// 发送是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 异常集合
        /// </summary>
        public Dictionary<IMessageInfo, Exception> Exceptions { get; private set; } = new Dictionary<IMessageInfo, Exception>();
    }
}
