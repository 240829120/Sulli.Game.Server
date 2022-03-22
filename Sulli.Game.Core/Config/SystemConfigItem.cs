using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 系统配置项
    /// </summary>
    public class SystemConfigItem
    {
        /// <summary>
        /// 键
        /// </summary>
        public string? Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// 属性集合
        /// </summary>
        public Dictionary<string, string> Attributes { get; private set; } = new Dictionary<string, string>();
    }
}
