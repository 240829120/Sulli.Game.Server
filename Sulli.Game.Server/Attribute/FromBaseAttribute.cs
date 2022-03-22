using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 数据源特性基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public abstract class FromBaseAttribute : Attribute
    {
        /// <summary>
        /// 类型
        /// </summary>
        public abstract FromType Type { get; }

        /// <summary>
        /// 名称
        /// </summary>
        public abstract string Name { get; set; }
    }
}
