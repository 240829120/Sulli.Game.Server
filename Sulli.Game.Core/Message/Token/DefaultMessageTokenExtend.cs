using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Message
{
    /// <summary>
    /// 默认消息标志扩展
    /// </summary>
    public static class DefaultMessageTokenExtend
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="manager">消息管理器</param>
        /// <param name="token">标志</param>
        /// <param name="owner">所属对象</param>
        /// <param name="action">行为</param>
        public static void Register<T>(this MessageManager manager, string token, object owner, Action<T> action) where T : class, IMessage
        {
            manager.Register<T>(new DefaultMessageToken<string>(token), owner, action);
        }

        /// <summary>
        /// 有顺序的发送
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="manager">消息管理器</param>
        /// <param name="token">标志</param>
        /// <param name="message">消息</param>
        /// <param name="isErrorBreak">发生异常时是否中断</param>
        /// <returns>任务</returns>
        public static async Task<MessageSendResult> SendOrderlyAsync<T>(this MessageManager manager, string token, T message, bool isErrorBreak = true) where T : class, IMessage
        {
            return await manager.SendOrderlyAsync<T>(new DefaultMessageToken<string>(token), message, isErrorBreak);
        }

        /// <summary>
        /// 并行发送
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="manager">消息管理器</param>
        /// <param name="token">标志</param>
        /// <param name="message">消息</param>
        /// <returns>任务</returns>
        public static async Task<MessageSendResult> SendParalleAsync<T>(this MessageManager manager, string token, T message) where T : class, IMessage
        {
            return await manager.SendParalleAsync<T>(new DefaultMessageToken<string>(token), message);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="manager">消息管理器</param>
        /// <param name="token">标志</param>
        /// <param name="owner">所属对象</param>
        /// <param name="action">行为</param>
        public static void Register<T>(this MessageManager manager, int token, object owner, Action<T> action) where T : class, IMessage
        {
            manager.Register<T>(new DefaultMessageToken<int>(token), owner, action);
        }

        /// <summary>
        /// 有顺序的发送
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="manager">消息管理器</param>
        /// <param name="token">标志</param>
        /// <param name="message">消息</param>
        /// <param name="isErrorBreak">发生异常时是否中断</param>
        /// <returns>任务</returns>
        public static async Task<MessageSendResult> SendOrderlyAsync<T>(this MessageManager manager, int token, T message, bool isErrorBreak = true) where T : class, IMessage
        {
            return await manager.SendOrderlyAsync<T>(new DefaultMessageToken<int>(token), message, isErrorBreak);
        }

        /// <summary>
        /// 并行发送
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="manager">消息管理器</param>
        /// <param name="token">标志</param>
        /// <param name="message">消息</param>
        /// <returns>任务</returns>
        public static async Task<MessageSendResult> SendParalleAsync<T>(this MessageManager manager, int token, T message) where T : class, IMessage
        {
            return await manager.SendParalleAsync<T>(new DefaultMessageToken<int>(token), message);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="manager">消息管理器</param>
        /// <param name="token">标志</param>
        /// <param name="owner">所属对象</param>
        /// <param name="action">行为</param>
        public static void Register<T>(this MessageManager manager, double token, object owner, Action<T> action) where T : class, IMessage
        {
            manager.Register<T>(new DefaultMessageToken<double>(token), owner, action);
        }

        /// <summary>
        /// 有顺序的发送
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="manager">消息管理器</param>
        /// <param name="token">标志</param>
        /// <param name="message">消息</param>
        /// <param name="isErrorBreak">发生异常时是否中断</param>
        /// <returns>任务</returns>
        public static async Task<MessageSendResult> SendOrderlyAsync<T>(this MessageManager manager, double token, T message, bool isErrorBreak = true) where T : class, IMessage
        {
            return await manager.SendOrderlyAsync<T>(new DefaultMessageToken<double>(token), message, isErrorBreak);
        }

        /// <summary>
        /// 并行发送
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="manager">消息管理器</param>
        /// <param name="token">标志</param>
        /// <param name="message">消息</param>
        /// <returns>任务</returns>
        public static async Task<MessageSendResult> SendParalleAsync<T>(this MessageManager manager, double token, T message) where T : class, IMessage
        {
            return await manager.SendParalleAsync<T>(new DefaultMessageToken<double>(token), message);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="manager">消息管理器</param>
        /// <param name="token">标志</param>
        /// <param name="owner">所属对象</param>
        /// <param name="action">行为</param>
        public static void Register<T>(this MessageManager manager, object token, object owner, Action<T> action) where T : class, IMessage
        {
            manager.Register<T>(new DefaultMessageToken<object>(token), owner, action);
        }

        /// <summary>
        /// 有顺序的发送
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="manager">消息管理器</param>
        /// <param name="token">标志</param>
        /// <param name="message">消息</param>
        /// <param name="isErrorBreak">发生异常时是否中断</param>
        /// <returns>任务</returns>
        public static async Task<MessageSendResult> SendOrderlyAsync<T>(this MessageManager manager, object token, T message, bool isErrorBreak = true) where T : class, IMessage
        {
            return await manager.SendOrderlyAsync<T>(new DefaultMessageToken<object>(token), message, isErrorBreak);
        }

        /// <summary>
        /// 并行发送
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="manager">消息管理器</param>
        /// <param name="token">标志</param>
        /// <param name="message">消息</param>
        /// <returns>任务</returns>
        public static async Task<MessageSendResult> SendParalleAsync<T>(this MessageManager manager, object token, T message) where T : class, IMessage
        {
            return await manager.SendParalleAsync<T>(new DefaultMessageToken<object>(token), message);
        }
    }
}
