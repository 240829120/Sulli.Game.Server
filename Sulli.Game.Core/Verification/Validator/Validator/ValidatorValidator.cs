using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 验证器验证
    /// </summary>
    public class ValidatorValidator<T> : ValidatorBase<T>
    {
        /// <summary>
        /// 验证器验证
        /// </summary>
        public ValidatorValidator(IValidatorContainer<T> context)
        {
            this.Context = context;
        }

        /// <summary>
        /// 验证器上下文
        /// </summary>
        public IValidatorContainer<T> Context { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="context">上下文</param>
        /// <returns>是否通过验证</returns>
        public override bool Verify(T value, ValidatorContext context)
        {
            if (!this.Context.Verify(value, context))
            {
                return false;
            }

            return true;
        }
    }
}
