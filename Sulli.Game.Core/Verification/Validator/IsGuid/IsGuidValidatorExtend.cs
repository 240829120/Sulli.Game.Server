using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 邮箱颜真器扩展
    /// </summary>
    public static class IsGuidValidatorExtend
    {
        /// <summary>
        /// 是否是GUID
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="context">值验证器上下文</param>
        /// <returns>值验证器上下文</returns>
        public static ValueValidatorContainer<T> IsGuid<T>(this ValueValidatorContainer<T> context)
        {
            context.Validators.Add(new IsGuidValidator<T>());

            return context;
        }

        /// <summary>
        /// 为真
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="context">值验证器上下文</param>
        /// <param name="isEmptyTrue">空GUID值是否通过验证</param>
        /// <returns>值验证器上下文</returns>
        public static ValueValidatorContainer<T> IsGuid<T>(this ValueValidatorContainer<T> context, bool isEmptyTrue)
        {
            context.Validators.Add(new IsGuidValidator<T>() { IsEmptyTrue = isEmptyTrue });

            return context;
        }

        /// <summary>
        /// 为真
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="context">值验证器上下文</param>
        /// <returns>值验证器上下文</returns>
        public static PropertyValidatorContainer<T, TProperty> IsGuid<T, TProperty>(this PropertyValidatorContainer<T, TProperty> context)
        {
            context.Validators.Add(new IsGuidValidator<TProperty>());

            return context;
        }

        /// <summary>
        /// 为真
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="context">值验证器上下文</param>
        /// <param name="isEmptyTrue">空GUID值是否通过验证</param>
        /// <returns>值验证器上下文</returns>
        public static PropertyValidatorContainer<T, TProperty> IsGuid<T, TProperty>(this PropertyValidatorContainer<T, TProperty> context, bool isEmptyTrue)
        {
            context.Validators.Add(new IsGuidValidator<TProperty>() { IsEmptyTrue = isEmptyTrue });

            return context;
        }
    }
}
