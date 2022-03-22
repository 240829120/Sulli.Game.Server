using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 循环验证器扩展
    /// </summary>
    public static class ForeachValidatorExtend
    {
        /// <summary>
        /// 为真
        /// </summary>
        /// <typeparam name="T">列表类型</typeparam>
        /// <typeparam name="TItem">项类型</typeparam>
        /// <param name="context">验证器上下文</param>
        /// <param name="cxt">项验证器上下文</param>
        /// <returns>值验证器上下文</returns>
        public static ValueValidatorContainer<T> Foreach<T, TItem>(this ValueValidatorContainer<T> context, IValidatorContainer<TItem> cxt) where T : IEnumerable<TItem>
        {
            context.Validators.Add(new ForeachValidator<T, TItem>(cxt));

            return context;
        }

        /// <summary>
        /// 为真
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <typeparam name="TItem">项类型</typeparam>
        /// <param name="context">验证器上下文</param>
        /// <param name="cxt">项验证器上下文</param>
        /// <returns>值验证器上下文</returns>
        public static PropertyValidatorContainer<T, TProperty> Foreach<T, TProperty, TItem>(this PropertyValidatorContainer<T, TProperty> context, IValidatorContainer<TItem> cxt) where TProperty : IEnumerable<TItem>
        {
            context.Validators.Add(new ForeachValidator<TProperty, TItem>(cxt));

            return context;
        }
    }
}
