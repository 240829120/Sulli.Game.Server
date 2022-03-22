using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 邮箱验证
    /// </summary>
    public class EmailValidator<T> : ValidatorBase<T>
    {
        /// <summary>
        /// 邮箱验证
        /// </summary>
        public EmailValidator()
        {

        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="context">验证上下文</param>
        /// <returns>是否通过验证</returns>
        public override bool Verify(T value, ValidatorContext context)
        {
            return RegexHelper.Email.IsMatch(value == null ? string.Empty : value.ToString());
        }
    }
}
