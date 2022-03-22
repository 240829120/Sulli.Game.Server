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
    /// 注释参数信息
    /// </summary>
    public class SummaryParamInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 参数信息
        /// </summary>
        [JsonIgnore]
        public ParameterInfo ParameterInfo { get; set; }
    }
}
