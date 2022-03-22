using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfig
    {
        /// <summary>
        /// 分组集合
        /// </summary>
        public Dictionary<string, SystemConfigGroup> Groups { get; private set; } = new Dictionary<string, SystemConfigGroup>();

        /// <summary>
        /// 获取或设置值
        /// </summary>
        /// <param name="group">分组</param>
        /// <param name="key"></param>
        /// <returns>值</returns>
        public string? this[string group, string key]
        {
            get { return this.GetValue(group, key); }
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="group">分组</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public string? GetValue(string group, string key)
        {
            return this.GetItem(group, key)?.Value;
        }

        /// <summary>
        /// 获取项
        /// </summary>
        /// <param name="group">组</param>
        /// <param name="key">键</param>
        /// <returns>项</returns>
        public SystemConfigItem? GetItem(string group, string key)
        {
            if (!this.Groups.TryGetValue(group, out SystemConfigGroup? configGroup))
                return null;

            if (!configGroup.Items.TryGetValue(key, out SystemConfigItem? configItem))
                return null;

            return configItem;
        }
    }
}
