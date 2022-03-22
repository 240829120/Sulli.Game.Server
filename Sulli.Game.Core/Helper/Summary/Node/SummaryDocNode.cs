using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sulli.Game.Core
{
    /// <summary>
    /// Doc节点
    /// </summary>
    public class SummaryDocNode
    {
        /// <summary>
        /// 程序集
        /// </summary>
        public SummaryAssemblyNode Assembly { get; set; }

        /// <summary>
        /// 成员
        /// </summary>
        public List<SummaryMemberNode> Members { get; set; } = new List<SummaryMemberNode>();

        /// <summary>
        /// 从XML元素中加载
        /// </summary>
        /// <param name="element">XML元素</param>
        /// <returns>Doc节点</returns>
        public static SummaryDocNode LoadFrom(XElement element)
        {
            if (element == null)
                return null;

            SummaryDocNode doc = new SummaryDocNode();
            doc.Assembly = SummaryAssemblyNode.LoadFrom(element.Element("assembly"));

            XElement members = element.Element("members");
            if (members == null)
                return doc;

            foreach (XElement item in members.Elements("member"))
            {
                SummaryMemberNode node = SummaryMemberNode.LoadFrom(item);
                if (node == null)
                    continue;

                doc.Members.Add(node);
            }

            return doc;
        }
    }
}
