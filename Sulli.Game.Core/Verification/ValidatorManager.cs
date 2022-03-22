using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 验证管理器
    /// </summary>
    public class ValidatorManager
    {
        /// <summary>
        /// 池子
        /// </summary>
        private readonly ConcurrentDictionary<Type, ValidatorTypeGroup> pool = new ConcurrentDictionary<Type, ValidatorTypeGroup>(Environment.ProcessorCount, 1000);

        /// <summary>
        /// 获取验证器容器
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="key">键</param>
        /// <returns>验证器容器</returns>
        public IValidatorContainer Get(Type type, string key)
        {
            if (!this.pool.TryGetValue(type, out ValidatorTypeGroup group))
                return null;

            if (!group.Containers.TryGetValue(key, out IValidatorContainer container))
                return null;

            return container;
        }

        /// <summary>
        /// 设置验证器容器
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="container">容器</param>
        public void Set<T>(string key, IValidatorContainer<T> container)
        {
            Type type = typeof(T);

            if (!this.pool.TryGetValue(type, out ValidatorTypeGroup group))
            {
                group = new ValidatorTypeGroup();

                this.pool.AddOrUpdate(type, group, (k, v) => group = v);
            }

            group.Containers.AddOrUpdate(key, container, (k, v) => container);
        }
    }
}
