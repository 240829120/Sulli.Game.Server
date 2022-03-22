using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 循环验证
    /// </summary>
    /// <typeparam name="T">列表类型</typeparam>
    /// <typeparam name="TItem">列表项类型</typeparam>
    public class ForeachValidator<T, TItem> : ValidatorBase<T> where T : IEnumerable<TItem>
    {
        /// <summary>
        /// 邮箱验证
        /// </summary>
        public ForeachValidator(IValidatorContainer<TItem> context)
        {
            this.Context = context;
        }

        /// <summary>
        /// 验证器上下文
        /// </summary>
        public IValidatorContainer<TItem> Context { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="context">上下文</param>
        /// <returns>是否通过验证</returns>
        public override bool Verify(T value, ValidatorContext context)
        {
            bool isPass = true;
            int index = -1;

            foreach (TItem item in value)
            {
                ++index;
                if (this.Context.Verify(item, context))
                {
                    continue;
                }

                context.Errors.Add($"item [{index}]: verify fail.");

                isPass = false;
            }

            return isPass;
        }
    }
}
