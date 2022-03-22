using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 方法上下文
    /// </summary>
    public class ActionContent
    {
        /// <summary>
        /// 数据
        /// </summary>
        public Dictionary<string, object> Data { get; private set; } = new Dictionary<string, object>();

        /// <summary>
        /// 拦截器集合
        /// </summary>
        public List<IFilter> Filters { get; private set; } = new List<IFilter>();
    }
}
