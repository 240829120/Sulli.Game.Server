using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 系统配置扩展
    /// </summary>
    public static class SystemConfigExtend
    {
        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="config">系统配置</param>
        public static void LoadConfigFromFile(this SystemConfig config)
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "system.config");
            config.LoadConfigFromFile(path);
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="config">系统配置</param>
        /// <param name="path">配置文件地址</param>
        public static void LoadConfigFromFile(this SystemConfig config, string path)
        {
            if (!System.IO.File.Exists(path))
                return;

            XElement root = null;
            using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                root = XElement.Load(fs);
            }
            foreach (XElement group in root.Elements(XName.Get("group", root.Name.NamespaceName)))
            {
                SystemConfigGroup configGroup = new SystemConfigGroup();
                configGroup.Name = group.Attribute(XName.Get("name", root.Name.NamespaceName))?.Value;

                if (string.IsNullOrWhiteSpace(configGroup.Name))
                {
                    continue;
                }

                foreach (XElement item in group.Elements(XName.Get("item", root.Name.NamespaceName)))
                {
                    SystemConfigItem configItem = new SystemConfigItem();

                    foreach (XAttribute attribute in item.Attributes())
                    {
                        configItem.Attributes[attribute.Name.LocalName] = attribute.Value;
                    }
                    configItem.Key = configItem.Attributes["key"];
                    configItem.Value = configItem.Attributes["value"];

                    configGroup.Items[configItem.Key] = configItem;
                }

                config.Groups[configGroup.Name] = configGroup;
            }
        }
    }
}
