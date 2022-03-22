using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 注释程序集信息
    /// </summary>
    public class SummaryAssemblyInfo
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型集合
        /// </summary>
        public List<SummaryTypeInfo> Types { get; set; } = new List<SummaryTypeInfo>();

        /// <summary>
        /// 程序集
        /// </summary>
        [JsonIgnore]
        public Assembly Assembly { get; set; }
    }
}
