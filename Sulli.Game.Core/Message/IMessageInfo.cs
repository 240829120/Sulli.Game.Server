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
    public interface IMessageInfo
    {
        /// <summary>
        /// 标志
        /// </summary>
        IMessageToken Token { get; }

        /// <summary>
        /// 消息类型
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// 所属对象
        /// </summary>
        WeakReference<object> Owner { get; }

        /// <summary>
        /// 行为
        /// </summary>
        Action<IMessage> Action { get; }
    }
}
