using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// GUID验证
    /// </summary>
    public class IsGuidValidator<T> : ValidatorBase<T>
    {
        /// <summary>
        /// 邮箱验证
        /// </summary>
        public IsGuidValidator()
        {

        }

        /// <summary>
        /// 空值是否通过验证
        /// </summary>
        public bool IsEmptyTrue { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="context">验证上下文</param>
        /// <returns>是否通过验证</returns>
        public override bool Verify(T value, ValidatorContext context)
        {
            if (value == null)
                return false;

            if (!Guid.TryParse(value.ToString(), out Guid guid))
                return false;

            if (!this.IsEmptyTrue && guid == Guid.Empty)
                return false;

            return true;
        }
    }
}
