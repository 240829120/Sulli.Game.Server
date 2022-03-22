using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 必填验证
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    public class RequiredValidator<T> : ValidatorBase<T>
    {
        /// <summary>
        /// 范围验证
        /// </summary>
        public RequiredValidator()
        {

        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="context">上下文</param>
        /// <returns>是否通过验证</returns>
        public override bool Verify(T value, ValidatorContext context)
        {
            if (value == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(value as string))
            {
                return false;
            }

            return true;
        }
    }
}