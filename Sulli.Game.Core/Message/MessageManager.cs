using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Message
{
    /// <summary>
    /// 消息管理器
    /// </summary>
    public class MessageManager
    {
        /// <summary>
        /// 日志
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(MessageManager));

        /// <summary>
        /// 容器
        /// </summary>
        private Dictionary<Type, MessageContainer> Containers = new Dictionary<Type, MessageContainer>();

        /// <summary>
        /// 锁对象
        /// </summary>
        private ReaderWriterLockSlim lock_object = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="token">标志</param>
        /// <param name="owner">所属对象</param>
        /// <param name="action">行为</param>
        public void Register<T>(IMessageToken token, object owner, Action<T> action) where T : class, IMessage
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            if (owner == null)
                throw new ArgumentNullException(nameof(owner));

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            Type type = typeof(T);

            MessageContainer container = null;

            LockHelper.UpgradeableRead(this.lock_object, () =>
            {
                this.Containers.TryGetValue(type, out container);
                if (container != null)
                    return;

                LockHelper.Write(this.lock_object, () =>
                {
                    container = new MessageContainer();
                    this.Containers.Add(type, container);
                });
            });

            LockHelper.Write(container.lock_object, () =>
            {
                container.Infos.TryGetValue(token, out List<IMessageInfo> list);
                if (list == null)
                {
                    list = new List<IMessageInfo>();
                    container.Infos[token] = list;
                }

                MessageInfo<T> messageInfo = new MessageInfo<T>();
                messageInfo.Type = type;
                messageInfo.Token = token;
                messageInfo.Owner = new WeakReference<object>(owner);
                messageInfo.Action = p => action(p as T);

                list.Add(messageInfo);
            });
        }

        /// <summary>
        /// 有顺序的发送
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="token">标志</param>
        /// <param name="message">消息</param>
        /// <param name="isErrorBreak">发生异常时是否中断</param>
        /// <returns>任务</returns>
        public async Task<MessageSendResult> SendOrderlyAsync<T>(IMessageToken token, T message, bool isErrorBreak = true) where T : class, IMessage
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            if (message == null)
                throw new ArgumentNullException(nameof(message));

            return await this.ExecuteSendOrderlyAsync(token, message, isErrorBreak);
        }

        /// <summary>
        /// 有顺序的发送
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="message">消息</param>
        /// <param name="isErrorBreak">发生异常时是否中断</param>
        /// <returns>任务</returns>
        public async Task<MessageSendResult> SendOrderlyAsync<T>(T message, bool isErrorBreak = true) where T : class, IMessage
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            return await this.ExecuteSendOrderlyAsync(null, message, isErrorBreak);
        }

        /// <summary>
        /// 并行发送
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="token">标志</param>
        /// <param name="message">消息</param>
        /// <returns>任务</returns>
        public async Task<MessageSendResult> SendParalleAsync<T>(IMessageToken token, T message) where T : class, IMessage
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            if (message == null)
                throw new ArgumentNullException(nameof(message));

            return await this.ExecuteSendParallelAsync(token, message);
        }

        /// <summary>
        /// 并行发送
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="message">消息</param>
        /// <returns>任务</returns>
        public async Task<MessageSendResult> SendParalleAsync<T>(T message) where T : class, IMessage
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            return await this.ExecuteSendParallelAsync(null, message);
        }

        /// <summary>
        /// 有顺序的发送
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="token">标志</param>
        /// <param name="message">消息</param>
        /// <param name="isErrorBreak">发生异常时是否中断</param>
        /// <returns>任务</returns>
        private async Task<MessageSendResult> ExecuteSendOrderlyAsync<T>(IMessageToken token, T message, bool isErrorBreak) where T : class, IMessage
        {
            return await Task.Run(() =>
            {
                MessageSendResult result = new MessageSendResult();

                List<IMessageInfo> infos = token == null ? this.GetMessageInfos<T>() : this.GetMessageInfos<T>(token);

                foreach (IMessageInfo info in infos)
                {
                    try
                    {
                        info.Action(message);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);

                        result.Exceptions.Add(info, ex);

                        if (isErrorBreak)
                            break;
                    }
                }

                result.IsSuccess = result.Exceptions.Count == 0;
                return result;
            });
        }

        /// <summary>
        /// 并行发送
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="token">标志</param>
        /// <param name="message">消息</param>
        /// <returns>任务</returns>
        private async Task<MessageSendResult> ExecuteSendParallelAsync<T>(IMessageToken token, T message) where T : class, IMessage
        {
            return await Task.Run(() =>
            {
                MessageSendResult result = new MessageSendResult();

                List<IMessageInfo> infos = token == null ? this.GetMessageInfos<T>() : this.GetMessageInfos<T>(token);

                Dictionary<Task, IMessageInfo> dic = new Dictionary<Task, IMessageInfo>();

                foreach (IMessageInfo info in infos)
                {
                    dic.Add(Task.Run(() => info.Action(message)), info);
                }

                Task.WaitAll(dic.Keys.ToArray());

                foreach (var kv in dic)
                {
                    if (kv.Key.Exception != null)
                    {
                        result.Exceptions[kv.Value] = kv.Key.Exception;
                    }
                }

                result.IsSuccess = result.Exceptions.Count == 0;
                return result;
            });
        }

        /// <summary>
        /// 获取消息信息列表
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="token">标志</param>
        /// <returns>消息信息列表</returns>
        private List<IMessageInfo> GetMessageInfos<T>(IMessageToken token) where T : class, IMessage
        {
            Type type = typeof(T);
            MessageContainer container = null;
            LockHelper.Read(this.lock_object, () =>
            {
                this.Containers.TryGetValue(type, out container);
            });
            if (container == null)
                return null;

            List<IMessageInfo> infos = null;
            LockHelper.Read(container.lock_object, () =>
            {
                container.Infos.TryGetValue(token, out infos);
                infos ??= infos.ToList();
            });

            return infos;
        }

        /// <summary>
        /// 获取消息信息列表
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <returns>消息信息列表</returns>
        private List<IMessageInfo> GetMessageInfos<T>() where T : class, IMessage
        {
            Type type = typeof(T);
            MessageContainer container = null;
            LockHelper.Read(this.lock_object, () =>
            {
                this.Containers.TryGetValue(type, out container);
            });
            if (container == null)
                return null;

            List<IMessageInfo> infos = new List<IMessageInfo>();
            LockHelper.Read(container.lock_object, () =>
            {
                foreach (var item in container.Infos.Values)
                {
                    infos.AddRange(item);
                }
            });

            return infos;
        }
    }
}
