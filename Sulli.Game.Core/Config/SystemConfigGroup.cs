using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 系统配置组
    /// </summary>
    public class SystemConfigGroup
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 项集合
        /// </summary>
        public Dictionary<string, SystemConfigItem> Items { get; private set; } = new Dictionary<string, SystemConfigItem>();
    }
}
