using Autofac;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 日志IOC参数构建器
    /// </summary>
    public class LogIocParameterBuildProvider : IAssemblyIocParameterBuildProvider
    {
        /// <summary>
        /// 是否可以获取值
        /// </summary>
        /// <param name="parameterInfo">参数信息</param>
        /// <param name="context">IOC上下文</param>
        /// <returns>是否可以获取值</returns>
        public bool CanValueAccessor(ParameterInfo parameterInfo, IComponentContext context)
        {
            return parameterInfo.ParameterType == typeof(ILog);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="parameterInfo">参数信息</param>
        /// <param name="context">IOC上下文</param>
        /// <returns>值</returns>
        public object ValueAccessor(ParameterInfo parameterInfo, IComponentContext context)
        {
            return LogManager.GetLogger(parameterInfo.ParameterType);
        }
    }
}
