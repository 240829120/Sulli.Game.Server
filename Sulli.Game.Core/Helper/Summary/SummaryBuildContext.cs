using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Sulli.Game.Core;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 注释构建上下文
    /// </summary>
    public class SummaryBuildContext
    {
        /// <summary>
        /// 当前处理的类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 当前处理的属性信息
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }

        /// <summary>
        /// 属性基础类型
        /// </summary>
        public Type PropertyBaseType { get; set; }

        /// <summary>
        /// 当前处理的类型注释信息
        /// </summary>
        public SummaryTypeInfo SummaryTypeInfo { get; set; }

        /// <summary>
        /// 当前处理的属性注释信息
        /// </summary>
        public SummaryPropertyInfo SummaryPropertyInfo { get; set; }

        /// <summary>
        /// 已经构建过的类型列表
        /// </summary>
        public List<Type> BuiledTypes { get; set; } = new List<Type>();
    }
}
