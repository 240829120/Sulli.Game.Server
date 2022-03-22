using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 类型验证容器
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class TypeValidatorContainer<T> : IValidatorContainer<T>
    {
        /// <summary>
        /// 验证器集合
        /// </summary>
        public List<IValidatorContainer<T>> Validators { get; private set; } = new List<IValidatorContainer<T>>();

        /// <summary>
        /// 执行验证
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="context">上下文</param>
        /// <returns>是否通过验证</returns>
        public bool Verify(T obj, ValidatorContext context)
        {
            if (this.Validators.Count == 0)
                return true;

            bool isPass = true;
            foreach (IValidatorContainer<T> validator in this.Validators)
            {
                if (validator.Verify(obj, context))
                {
                    continue;
                }

                isPass = false;
            }

            return isPass;
        }
    }
}
