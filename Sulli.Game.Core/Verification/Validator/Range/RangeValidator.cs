using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 范围验证
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    public class RangeValidator<T> : ValidatorBase<T> where T : struct, IComparable<T>
    {
        /// <summary>
        /// 范围验证
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        public RangeValidator(T? minValue, T? maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public T? MinValue { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public T? MaxValue { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="context">上下文</param>
        /// <returns>是否通过验证</returns>
        public override bool Verify(T value, ValidatorContext context)
        {
            if (this.MinValue != null && value.CompareTo(this.MinValue.Value) < 0)
                return false;

            if (this.MaxValue != null && value.CompareTo(this.MaxValue.Value) > 0)
                return false;

            return true;
        }
    }
}
