using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 值验证容器
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    public class ValueValidatorContainer<T> : IValidatorContainer<T>
    {
        /// <summary>
        /// 验证器集合
        /// </summary>
        public List<IValidator<T>> Validators { get; private set; } = new List<IValidator<T>>();

        /// <summary>
        /// 执行验证
        /// </summary>
        /// <returns>是否通过验证</returns>
        public bool Verify(T value, ValidatorContext context)
        {
            if (this.Validators.Count == 0)
                return true;

            bool isPass = true;

            foreach (IValidator<T> validator in this.Validators)
            {
                if (validator.Verify(value, context))
                {
                    continue;
                }

                if (validator.MessageFunc != null)
                {
                    context.Errors.Add(validator.MessageFunc(value));
                    isPass = false;
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(validator.Message))
                {
                    context.Errors.Add(validator.Message);
                    isPass = false;
                    continue;
                }

                context.Errors.Add($"value: [{value}] verify fail.");
                isPass = false;
            }

            return isPass;
        }
    }
}
