using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Message
{
    /// <summary>
    /// 默认消息标志
    /// </summary>
    public class DefaultMessageToken<T> : IMessageToken
    {
        /// <summary>
        /// 对象消息类型标志
        /// </summary>
        /// <param name="value"></param>
        public DefaultMessageToken(T value)
        {
            this.Value = value;
        }

        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// 对象是否相等
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object obj)
        {
            if (this.Value == null || obj == null)
                return false;

            return this.GetHashCode() == obj.GetHashCode();
        }

        /// <summary>
        /// 获取HashCode
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}
