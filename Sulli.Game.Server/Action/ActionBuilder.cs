using Autofac;
using Autofac.Core;
using Sulli.Game.Core;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 方法构建器
    /// </summary>
    public class ActionBuilder : IRouteBuilder
    {
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="engine">引擎</param>
        /// <param name="roots">路由根节点</param>
        /// <param name="actions">行为集合</param>
        public void Build(ServerEngine engine, Dictionary<ActionType, RouteNode> roots, List<ActionInfo> actions)
        {
            RouteHelper.Traverse(roots, node =>
            {
                if (node.ActionInfo == null)
                    return true;

                AssemblyHelper.IocBuildWithParameters(node.ActionInfo.ControllerType, node.ActionInfo.ControllerType, Application.Current.ContainerBuilder).InstancePerLifetimeScope();

                return true;
            });
        }
    }
}
