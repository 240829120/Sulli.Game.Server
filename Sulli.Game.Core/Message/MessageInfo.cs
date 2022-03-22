using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Message
{
    /// <summary>
    /// 消息信息
    /// </summary>
    public class MessageInfo<T> : IMessageInfo where T : IMessage
    {
        /// <summary>
        /// 标志
        /// </summary>
        public IMessageToken Token { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 所属对象
        /// </summary>
        public WeakReference<object> Owner { get; set; }

        /// <summary>
        /// 行为
        /// </summary>
        public Action<IMessage> Action { get; set; }
    }
}
