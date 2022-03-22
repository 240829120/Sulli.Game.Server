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
    /// 注释方法信息
    /// </summary>
    public class SummaryMethodInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string? Summary { get; set; }

        /// <summary>
        /// 返回
        /// </summary>
        public string? Returns { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public List<SummaryParamInfo> Params { get; set; } = new List<SummaryParamInfo>();

        /// <summary>
        /// 方法信息
        /// </summary>
        [JsonIgnore]
        public MethodInfo? MethodInfo { get; set; }
    }
}
