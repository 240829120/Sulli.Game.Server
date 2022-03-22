using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 拦截器上下文
    /// </summary>
    public class FilterContext
    {
        /// <summary>
        /// 拦截器上下文
        /// </summary>
        /// <param name="engine">服务引擎</param>
        /// <param name="actionInfo">当前请求的方法</param>
        /// <param name="filters">拦截器集合</param>
        public FilterContext(ServerEngine engine, ActionInfo actionInfo, List<IFilter> filters)
        {
            this.Engine = engine;
            this.ActionInfo = actionInfo;
            this.Filters = filters;
        }

        /// <summary>
        /// 数据
        /// </summary>
        public Dictionary<string, object> Data { get; private set; } = new Dictionary<string, object>();

        /// <summary>
        /// 拦截器集合
        /// </summary>
        public List<IFilter> Filters { get; private set; }

        /// <summary>
        /// 服务引擎
        /// </summary>
        public ServerEngine Engine { get; private set; }

        /// <summary>
        /// 当前请求的方法
        /// </summary>
        public ActionInfo ActionInfo { get; private set; }

        /// <summary>
        /// 是否继续执行
        /// </summary>
        public bool IsContinue { get; set; } = true;

        /// <summary>
        /// 异常信息
        /// </summary>
        public ServerException Exception { get; set; }
    }
}
