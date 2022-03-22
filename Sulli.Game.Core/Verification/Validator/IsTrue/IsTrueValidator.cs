using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 为真验证
    /// </summary>
    public class IsTrueValidator<T> : ValidatorBase<T>
    {
        /// <summary>
        /// 为真验证
        /// </summary>
        /// <param name="expression">表达式</param>
        public IsTrueValidator(Func<T, bool> expression)
        {
            this.Expression = expression;
        }

        /// <summary>
        /// 表达式
        /// </summary>
        public Func<T, bool> Expression { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="context">上下文</param>
        /// <returns>是否通过验证</returns>
        public override bool Verify(T value, ValidatorContext context)
        {
            return this.Expression.Invoke(value);
        }
    }
}
