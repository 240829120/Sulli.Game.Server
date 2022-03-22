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
    /// 属性验证容器
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <typeparam name="TProperty">属性类型</typeparam>
    public class PropertyValidatorContainer<T, TProperty> : IValidatorContainer<T>
    {
        /// <summary>
        /// 验证器集合
        /// </summary>
        public List<IValidator<TProperty>> Validators { get; private set; } = new List<IValidator<TProperty>>();

        /// <summary>
        /// 获取属性值方法
        /// </summary>
        public Func<T, TProperty> GetPropertyFunc { get; internal set; }

        /// <summary>
        /// 属性名
        /// </summary>
        public PropertyInfo PropertyInfo { get; internal set; }

        /// <summary>
        /// 所属类型验证
        /// </summary>
        public TypeValidatorContainer<T> Owner { get; internal set; }

        /// <summary>
        /// 执行验证
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="context">上下文</param>
        /// <returns>是否通过验证</returns>
        public bool Verify(T obj, ValidatorContext context)
        {
            if (this.Validators.Count == 0 || this.GetPropertyFunc == null)
                return true;

            TProperty value = this.GetPropertyFunc.Invoke(obj);
            bool isPass = true;

            foreach (IValidator<TProperty> validator in this.Validators)
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

                context.Errors.Add($" value: [{value}] verify fail.");
                isPass = false;
            }

            return isPass;
        }
    }
}
