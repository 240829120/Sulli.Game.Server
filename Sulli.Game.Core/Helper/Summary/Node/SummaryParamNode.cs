using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 参数节点
    /// </summary>
    public class SummaryParamNode
    {
        /// <summary>
        /// 参数名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 从XML元素中加载
        /// </summary>
        /// <param name="element">XML元素</param>
        /// <returns>Member节点</returns>
        public static SummaryParamNode LoadFrom(XElement element)
        {
            if (element == null)
                return null;

            SummaryParamNode node = new SummaryParamNode();

            node.Name = element.Attribute("name")?.Value?.Trim();
            node.Content = element.Value?.Trim();

            return node;
        }
    }
}
