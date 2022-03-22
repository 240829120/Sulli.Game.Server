using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 验证器验证扩展
    /// </summary>
    public static class ValidatorValidatorExtend
    {
        /// <summary>
        /// 验证器验证
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="context">验证器上下文</param>
        /// <param name="cxt">验证器上下文</param>
        /// <returns>值验证器上下文</returns>
        public static ValueValidatorContainer<T> Type<T>(this ValueValidatorContainer<T> context, IValidatorContainer<T> cxt) where T : IComparable<T>
        {
            context.Validators.Add(new ValidatorValidator<T>(cxt));

            return context;
        }

        /// <summary>
        /// 类型验证
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="context">验证器上下文</param>
        /// <param name="cxt">验证器上下文</param>
        /// <returns>值验证器上下文</returns>
        public static PropertyValidatorContainer<T, TProperty> Type<T, TProperty>(this PropertyValidatorContainer<T, TProperty> context, IValidatorContainer<TProperty> cxt)
        {
            context.Validators.Add(new ValidatorValidator<TProperty>(cxt));

            return context;
        }
    }
}
