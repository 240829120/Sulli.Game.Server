using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 正则表达式验证
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    public class RegexValidator<T> : ValidatorBase<T>
    {
        /// <summary>
        /// 范围验证
        /// </summary>
        /// <param name="regex">正则表达式</param>
        public RegexValidator(Regex regex)
        {
            this.Regex = regex;
        }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public Regex Regex { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="context">上下文</param>
        /// <returns>是否通过验证</returns>
        public override bool Verify(T value, ValidatorContext context)
        {
            return this.Regex.IsMatch(value == null ? string.Empty : value.ToString());
        }
    }
}
