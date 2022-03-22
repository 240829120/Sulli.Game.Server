using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 注释类型信息
    /// </summary>
    public class SummaryTypeInfo
    {
        /// <summary>
        /// 完整名称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 属性集合
        /// </summary>
        public List<SummaryPropertyInfo> Propertys { get; set; } = new List<SummaryPropertyInfo>();

        /// <summary>
        /// 字段集合
        /// </summary>
        public List<SummaryFieldInfo> Fields { get; set; } = new List<SummaryFieldInfo>();

        /// <summary>
        /// 方法集合
        /// </summary>
        public List<SummaryMethodInfo> Methods { get; set; } = new List<SummaryMethodInfo>();

        /// <summary>
        /// 类型
        /// </summary>
        [JsonIgnore]
        public Type Type { get; set; }
    }
}
