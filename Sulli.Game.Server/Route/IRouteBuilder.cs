using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 路由构建器
    /// </summary>
    public interface IRouteBuilder
    {
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="engine">引擎</param>
        /// <param name="roots">路由根节点</param>
        /// <param name="actions">行为集合</param>
        void Build(ServerEngine engine, Dictionary<ActionType, RouteNode> roots, List<ActionInfo> actions);
    }
}
