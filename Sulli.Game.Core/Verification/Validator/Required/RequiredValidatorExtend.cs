using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 必填验证扩展
    /// </summary>
    public static class RequiredValidator
    {
        /// <summary>
        /// 必填
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="context">值验证器上下文</param>
        /// <returns>值验证器上下文</returns>
        public static ValueValidatorContainer<T> Required<T>(this ValueValidatorContainer<T> context) where T : IComparable<T>
        {
            context.Validators.Add(new RequiredValidator<T>());

            return context;
        }

        /// <summary>
        /// 必填
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="context">值验证器上下文</param>
        /// <returns>值验证器上下文</returns>
        public static PropertyValidatorContainer<T, TProperty> Required<T, TProperty>(this PropertyValidatorContainer<T, TProperty> context)
        {
            context.Validators.Add(new RequiredValidator<TProperty>());

            return context;
        }
    }
}
