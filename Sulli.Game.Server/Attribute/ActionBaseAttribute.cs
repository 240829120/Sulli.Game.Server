using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 方法基础属性
    /// </summary>
    public abstract class ActionBaseAttribute : Attribute
    {
        /// <summary>
        /// 方法类型
        /// </summary>
        public abstract ActionType Type { get; }

        /// <summary>
        /// 获取支持的方法集合
        /// </summary>
        /// <returns>支持的方法集合</returns>
        public abstract List<string> GetMethods();
    }
}
