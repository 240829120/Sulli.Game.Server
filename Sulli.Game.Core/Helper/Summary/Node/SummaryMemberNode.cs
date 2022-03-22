using Sulli.Game.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using log4net;

namespace Sulli.Game.Core
{
    /// <summary>
    /// Member节点
    /// </summary>
    public class SummaryMemberNode
    {
        /// <summary>
        /// 日志
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(SummaryMemberNode));

        /// <summary>
        /// Member节点类型
        /// </summary>
        public SummaryMemberType Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 概要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 参数集合
        /// </summary>
        public List<SummaryParamNode> Params { get; set; } = new List<SummaryParamNode>();

        /// <summary>
        /// 返回
        /// </summary>
        public string Returns { get; set; }

        /// <summary>
        /// 从XML元素中加载
        /// </summary>
        /// <param name="element">XML元素</param>
        /// <returns>Member节点</returns>
        public static SummaryMemberNode LoadFrom(XElement element)
        {
            if (element == null)
                return null;

            SummaryMemberNode node = new SummaryMemberNode();

            string nameValue = element.Attribute("name")?.Value?.Trim();
            if (string.IsNullOrWhiteSpace(nameValue))
                return null;

            try
            {
                node.Type = EnumHelper.GetEnumFromName<SummaryMemberType>(nameValue.Substring(0, 1));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
            node.Name = nameValue[2..];
            node.Summary = element.Element("summary")?.Value?.Trim();
            node.Returns = element.Element("returns")?.Value?.Trim();

            foreach (XElement item in element.Elements("param"))
            {
                node.Params.Add(SummaryParamNode.LoadFrom(item));
            }

            return node;
        }
    }
}
