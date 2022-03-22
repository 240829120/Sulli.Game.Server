using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 方法参数信息
    /// </summary>
    public class ActionParameterInfo
    {
        /// <summary>
        /// 参数信息
        /// </summary>
        public ParameterInfo ParameterInfo { get; set; }

        /// <summary>
        /// 源特性
        /// </summary>
        public FromBaseAttribute FromAttribute { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public Dictionary<string, object> Data { get; private set; } = new Dictionary<string, object>();
    }
}
