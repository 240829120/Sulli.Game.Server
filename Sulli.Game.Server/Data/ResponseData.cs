using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 返回数据
    /// </summary>
    public class ResponseData
    {
        /// <summary>
        /// 请求ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 类型
        /// <see cref="ResponseDataType"/>
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }
    }
}
