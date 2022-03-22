using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Message
{
    /// <summary>
    /// 消息信息
    /// </summary>
    public class MessageContainer
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 锁对象
        /// </summary>
        public ReaderWriterLockSlim lock_object = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

        /// <summary>
        /// 消息信息集合
        /// </summary>
        public Dictionary<IMessageToken, List<IMessageInfo>> Infos { get; private set; } = new Dictionary<IMessageToken, List<IMessageInfo>>();
    }
}
