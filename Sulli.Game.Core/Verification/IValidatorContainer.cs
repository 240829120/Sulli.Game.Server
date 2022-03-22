using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 验证容器
    /// </summary>
    public interface IValidatorContainer
    {

    }

    /// <summary>
    /// 验证容器
    /// </summary>
    public interface IValidatorContainer<T> : IValidatorContainer
    {
        /// <summary>
        /// 执行验证
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="context">上下文</param>
        /// <returns>是否通过验证</returns>
        bool Verify(T obj, ValidatorContext context);
    }
}
