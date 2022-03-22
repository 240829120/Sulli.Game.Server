using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 验证器基类
    /// </summary>
    public abstract class ValidatorBase<T> : IValidator<T>
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 消息方法
        /// </summary>
        public Func<T, string> MessageFunc { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="context">验证上下文</param>
        /// <returns>是否通过验证</returns>
        public abstract bool Verify(T value, ValidatorContext context);
    }
}
