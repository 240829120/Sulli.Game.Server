using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 正则表达式验证扩展
    /// </summary>
    public static class RegexValidatorExtend
    {
        /// <summary>
        /// 正则表达
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="context">值验证器上下文</param>
        /// <param name="regex">正则表达式</param>
        /// <returns>值验证器上下文</returns>
        public static ValueValidatorContainer<T> Regex<T>(this ValueValidatorContainer<T> context, Regex regex)
        {
            context.Validators.Add(new RegexValidator<T>(regex));

            return context;
        }

        /// <summary>
        /// 正则表达
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="context">值验证器上下文</param>
        /// <param name="regex">正则表达式</param>
        /// <returns>值验证器上下文</returns>
        public static ValueValidatorContainer<T> Regex<T>(this ValueValidatorContainer<T> context, string regex)
        {
            context.Validators.Add(new RegexValidator<T>(new Regex(regex)));

            return context;
        }

        /// <summary>
        /// 正则表达
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="context">值验证器上下文</param>
        /// <param name="regex">正则表达式</param>
        /// <returns>值验证器上下文</returns>
        public static PropertyValidatorContainer<T, TProperty> Regex<T, TProperty>(this PropertyValidatorContainer<T, TProperty> context, Regex regex)
        {
            context.Validators.Add(new RegexValidator<TProperty>(regex));

            return context;
        }

        /// <summary>
        /// 正则表达
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="context">值验证器上下文</param>
        /// <param name="regex">正则表达式</param>
        /// <returns>值验证器上下文</returns>
        public static PropertyValidatorContainer<T, TProperty> Regex<T, TProperty>(this PropertyValidatorContainer<T, TProperty> context, string regex)
        {
            context.Validators.Add(new RegexValidator<TProperty>(new Regex(regex)));

            return context;
        }
    }
}
