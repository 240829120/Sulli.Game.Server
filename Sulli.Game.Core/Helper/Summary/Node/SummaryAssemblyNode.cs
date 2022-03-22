using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sulli.Game.Core
{
    /// <summary>
    /// Assembly节点
    /// </summary>
    public class SummaryAssemblyNode
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 从XML元素中加载
        /// </summary>
        /// <param name="element">XML元素</param>
        /// <returns>Assembly节点</returns>
        public static SummaryAssemblyNode LoadFrom(XElement element)
        {
            if (element == null)
                return null;

            SummaryAssemblyNode node = new SummaryAssemblyNode();

            node.Name = element.Element("name")?.Value?.Trim();

            return node;
        }
    }
}
