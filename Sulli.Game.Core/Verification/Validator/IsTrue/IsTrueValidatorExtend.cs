using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 为真验证器扩展
    /// </summary>
    public static class IsTrueValidatorExtend
    {
        /// <summary>
        /// 为真
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="context">值验证器上下文</param>
        /// <param name="expression">判断条件</param>
        /// <returns>值验证器上下文</returns>
        public static ValueValidatorContainer<T> IsTrue<T>(this ValueValidatorContainer<T> context, Func<T, bool> expression)
        {
            context.Validators.Add(new IsTrueValidator<T>(expression));

            return context;
        }

        /// <summary>
        /// 为真
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="context">值验证器上下文</param>
        /// <param name="expression">判断条件</param>
        /// <returns>值验证器上下文</returns>
        public static PropertyValidatorContainer<T, TProperty> IsTrue<T, TProperty>(this PropertyValidatorContainer<T, TProperty> context, Func<TProperty, bool> expression)
        {
            context.Validators.Add(new IsTrueValidator<TProperty>(expression));

            return context;
        }
    }
}
