using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 范围验证扩展
    /// </summary>
    public static class RangeValidatorExtend
    {
        /// <summary>
        /// 范围
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="context">值验证器上下文</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>值验证器上下文</returns>
        public static ValueValidatorContainer<T> Range<T>(this ValueValidatorContainer<T> context, T? minValue, T? maxValue) where T : struct, IComparable<T>
        {
            context.Validators.Add(new RangeValidator<T>(minValue, maxValue));

            return context;
        }

        /// <summary>
        /// 范围
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="context">值验证器上下文</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>值验证器上下文</returns>
        public static PropertyValidatorContainer<T, TProperty> Range<T, TProperty>(this PropertyValidatorContainer<T, TProperty> context, TProperty? minValue, TProperty? maxValue) where TProperty : struct, IComparable<TProperty>
        {
            context.Validators.Add(new RangeValidator<TProperty>(minValue, maxValue));

            return context;
        }
    }
}
