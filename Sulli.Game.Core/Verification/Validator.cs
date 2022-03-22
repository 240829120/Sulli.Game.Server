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
    /// 验证器
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// 默认键
        /// </summary>
        public const string DEFAULT_KEY = "Default";

        /// <summary>
        /// 创建值验证
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <returns>值验证器上下文</returns>
        public static ValueValidatorContainer<T> CreateValue<T>()
        {
            return new ValueValidatorContainer<T>();
        }

        /// <summary>
        /// 创建类型验证
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>类型验证器上下文</returns>
        public static TypeValidatorContainer<T> CreateType<T>()
        {
            return new TypeValidatorContainer<T>();
        }

        /// <summary>
        /// 获取值验证器
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>值验证</returns>
        public static ValueValidatorContainer<T> GetValue<T>(string key)
        {
            return Application.Current.ValidatorManager.Get(typeof(T), key) as ValueValidatorContainer<T>;
        }

        /// <summary>
        /// 获取值验证器
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <returns>值验证</returns>
        public static ValueValidatorContainer<T> GetValue<T>()
        {
            return GetValue<T>(DEFAULT_KEY);
        }

        /// <summary>
        /// 获取类型验证器
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>类型验证</returns>
        public static TypeValidatorContainer<T> GetType<T>(string key)
        {
            return Application.Current.ValidatorManager.Get(typeof(T), key) as TypeValidatorContainer<T>;
        }

        /// <summary>
        /// 获取类型验证器
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>类型验证</returns>
        public static TypeValidatorContainer<T> GetType<T>()
        {
            return GetType<T>(DEFAULT_KEY);
        }

        /// <summary>
        /// 获取或创建值验证
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="createFunc">值验证创建方法</param>
        /// <returns>值验证</returns>
        public static ValueValidatorContainer<T> GetOrCreate<T>(string key, Func<ValueValidatorContainer<T>> createFunc)
        {
            IValidatorContainer container = Application.Current.ValidatorManager.Get(typeof(T), key);

            if (container != null)
                return container as ValueValidatorContainer<T>;

            ValueValidatorContainer<T> newContainer = createFunc();
            Application.Current.ValidatorManager.Set<T>(key, newContainer);

            return newContainer;
        }

        /// <summary>
        /// 获取或创建值验证
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="createFunc">值验证创建方法</param>
        /// <returns>值验证</returns>
        public static ValueValidatorContainer<T> GetOrCreate<T>(Func<ValueValidatorContainer<T>> createFunc)
        {
            return GetOrCreate<T>(DEFAULT_KEY, createFunc);
        }

        /// <summary>
        /// 获取或创建类型验证
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="createFunc">类型验证创建方法</param>
        /// <returns>类型验证</returns>
        public static TypeValidatorContainer<T> GetOrCreate<T>(string key, Func<TypeValidatorContainer<T>> createFunc)
        {
            IValidatorContainer container = Application.Current.ValidatorManager.Get(typeof(T), key);

            if (container != null)
                return container as TypeValidatorContainer<T>;

            TypeValidatorContainer<T> newContainer = createFunc();
            Application.Current.ValidatorManager.Set<T>(key, newContainer);

            return newContainer;
        }

        /// <summary>
        /// 获取或创建类型验证
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="createFunc">类型验证创建方法</param>
        /// <returns>类型验证</returns>
        public static TypeValidatorContainer<T> GetOrCreate<T>(Func<TypeValidatorContainer<T>> createFunc)
        {
            return GetOrCreate<T>(DEFAULT_KEY, createFunc);
        }
    }
}
