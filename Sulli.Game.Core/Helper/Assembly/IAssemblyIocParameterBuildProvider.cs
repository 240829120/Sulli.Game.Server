using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 程序集Ioc参数构建
    /// </summary>
    public interface IAssemblyIocParameterBuildProvider
    {
        /// <summary>
        /// 是否可以获取值
        /// </summary>
        /// <param name="parameterInfo">参数信息</param>
        /// <param name="context">IOC上下文</param>
        /// <returns>是否可以获取值</returns>
        bool CanValueAccessor(ParameterInfo parameterInfo, IComponentContext context);

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="parameterInfo">参数信息</param>
        /// <param name="context">IOC上下文</param>
        /// <returns>值</returns>
        object ValueAccessor(ParameterInfo parameterInfo, IComponentContext context);
    }
}
