using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 属性验证容器扩展
    /// </summary>
    public static class PropertyValidatorContainerExtend
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="TProperty">对象属性</typeparam>
        /// <param name="container">容器</param>
        /// <param name="message">消息</param>
        /// <returns>容器</returns>
        public static PropertyValidatorContainer<T, TProperty> WithMessage<T, TProperty>(this PropertyValidatorContainer<T, TProperty> container, string message)
        {
            IValidator<TProperty> validator = container.Validators.LastOrDefault();
            if (validator != null)
            {
                validator.Message = message;
            }

            return container;
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <typeparam name="TProperty">对象属性</typeparam>
        /// <param name="container">容器</param>
        /// <param name="format">格式话字符串</param>
        /// <param name="args">格式话字符串参数</param>
        /// <returns>值验证器</returns>
        public static PropertyValidatorContainer<T, TProperty> WithMessage<T, TProperty>(this PropertyValidatorContainer<T, TProperty> container, string format, params object[] args)
        {
            IValidator<TProperty> validator = container.Validators.LastOrDefault();
            if (validator != null)
            {
                validator.Message = string.Format(format, args);
            }

            return container;
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <typeparam name="TProperty">对象属性</typeparam>
        /// <param name="container">容器</param>
        /// <param name="messageFunc">消息方法</param>
        /// <returns>容器</returns>
        public static PropertyValidatorContainer<T, TProperty> WithMessage<T, TProperty>(this PropertyValidatorContainer<T, TProperty> container, Func<TProperty, string> messageFunc)
        {
            IValidator<TProperty> validator = container.Validators.LastOrDefault();
            if (validator != null)
            {
                validator.MessageFunc = messageFunc;
            }

            return container;
        }
    }
}
