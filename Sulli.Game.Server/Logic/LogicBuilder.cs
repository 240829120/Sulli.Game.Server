using Autofac;
using Autofac.Core;
using Sulli.Game.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 逻辑路由构建器
    /// </summary>
    public class LogicBuilder : IRouteBuilder
    {
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="engine">引擎</param>
        /// <param name="roots">路由根节点</param>
        /// <param name="actions">行为集合</param>
        public void Build(ServerEngine engine, Dictionary<ActionType, RouteNode> roots, List<ActionInfo> actions)
        {
            AssemblyHelper.Foreach((assembly, type) =>
            {
                this.AppendLogic(engine.Option, type);
            });
        }

        /// <summary>
        /// 添加逻辑
        /// </summary>
        /// <param name="option">添加逻辑</param>
        /// <param name="type">类型</param>
        private void AppendLogic(ServerOption option, Type type)
        {
            if (type.IsAbstract || type.IsInterface)
                return;

            Type logicTpye = typeof(IServerLogic);

            if (!logicTpye.IsAssignableFrom(type))
                return;

            Type[] interfaceTypes = type.GetInterfaces();

            foreach (Type interfaceType in interfaceTypes)
            {
                if (!logicTpye.IsAssignableFrom(interfaceType))
                    continue;

                AssemblyHelper.IocBuildWithParameters(type, interfaceType, Application.Current.ContainerBuilder).SingleInstance();
            }
        }
    }
}
