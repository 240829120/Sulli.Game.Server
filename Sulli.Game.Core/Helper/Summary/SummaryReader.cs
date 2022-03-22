using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 注释读取器
    /// </summary>
    public class SummaryReader
    {
        /// <summary>
        /// 日志
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(SummaryReader));

        /// <summary>
        /// 加载API文档
        /// </summary>
        /// <param name="path">扫描文件夹路径</param>
        /// <returns>文档集合</returns>
        public List<SummaryDocNode> Load(string path)
        {
            string[] files = System.IO.Directory.GetFiles(path, "*.xml");

            List<SummaryDocNode> list = new List<SummaryDocNode>();

            foreach (string file in files)
            {
                SummaryDocNode doc = this.LoadFile(file);
                if (doc == null)
                    continue;

                list.Add(doc);
            }

            return list;
        }

        /// <summary>
        /// 加载API文档
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>API文档</returns>
        private SummaryDocNode LoadFile(string file)
        {
            try
            {
                XElement root = null;
                using (System.IO.StreamReader sr = new System.IO.StreamReader(file, Encoding.UTF8))
                {
                    root = XElement.Load(sr);
                }

                return SummaryDocNode.LoadFrom(root);
            }
            catch (Exception ex)
            {
                log.Error(ex);

                return null;
            }
        }
    }
}
