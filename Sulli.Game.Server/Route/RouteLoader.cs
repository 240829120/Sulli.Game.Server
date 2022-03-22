using Autofac;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 路由加载器
    /// </summary>
    public class RouteLoader
    {
        /// <summary>
        /// 日志
        /// </summary>
        private ILogger logger = LoggerManager.GetLogger(typeof(RouteLoader).Assembly, typeof(RouteLoader));

        /// <summary>
        /// 根节点集合
        /// </summary>
        public Dictionary<ActionType, RouteNode> Roots { get; private set; }

        /// <summary>
        /// 内置路由构建器
        /// </summary>
        private List<IRouteBuilder> InnerRouteBuilders = new List<IRouteBuilder>();

        /// <summary>
        /// 引擎
        /// </summary>
        private ServerEngine Engine;

        /// <summary>
        /// 路由加载器
        /// </summary>
        /// <param name="engine">引擎</param>
        public RouteLoader(ServerEngine engine)
        {
            this.Engine = engine;

            this.InnerRouteBuilders.Add(new FilterBuilder());
            this.InnerRouteBuilders.Add(new ActionBuilder());
            this.InnerRouteBuilders.Add(new LogicBuilder());
        }

        /// <summary>
        /// 构建路由
        /// </summary>
        /// <param name="actions"></param>
        public void Build(List<ActionInfo> actions)
        {
            // 初始化根节点
            this.InitRoots();

            // 初始化方法路由
            this.InitActionInfo(actions);

            // 执行内置构建器
            foreach (IRouteBuilder builder in this.InnerRouteBuilders)
            {
                builder.Build(this.Engine, this.Roots, actions);
            }

            // 执行外部构建器
            foreach (IRouteBuilder builder in this.Engine.RouteBuilders)
            {
                builder.Build(this.Engine, this.Roots, actions);
            }
        }

        /// <summary>
        /// 初始化Root
        /// </summary>
        private void InitRoots()
        {
            Type type = typeof(ActionType);
            this.Roots = new Dictionary<ActionType, RouteNode>();

            foreach (string name in Enum.GetNames(type))
            {
                RouteNode node = new RouteNode();
                node.IsRoot = true;
                node.Value = $"{name.ToLower()}:///";

                this.Roots[(ActionType)Enum.Parse(type, name)] = node;
            }
        }

        /// <summary>
        /// 初始化方法路由
        /// </summary>
        /// <param name="actions">方法信息</param>
        private void InitActionInfo(List<ActionInfo> actions)
        {
            if (actions == null || actions.Count == 0)
                return;

            foreach (ActionInfo action in actions)
            {
                RouteNode node = this.GetRoute(action.Type, $"{action.ControllerRoute.Path}/{action.ActionRoute.Path}");

                node.ActionInfo = action;
                node.IsLeaf = true;
            }
        }

        /// <summary>
        /// 获取或者创建路由信息
        /// </summary>
        /// <param name="type">方法类型</param>
        /// <param name="path">路径</param>
        /// <returns>路由节点</returns>
        private RouteNode GetRoute(ActionType type, string path)
        {
            RouteNode root = this.Roots[type];
            Queue<string> queue = this.GetPathQueue(path);
            RouteNode node = _GetRoute(root, queue);

            return node;
        }

        /// <summary>
        /// 创建或者获取路由节点
        /// </summary>
        /// <param name="node">路由节点</param>
        /// <param name="queue">路径值队列</param>
        /// <returns>路由节点</returns>
        private RouteNode _GetRoute(RouteNode node, Queue<string> queue)
        {
            if (queue.Count == 0)
                return node;

            string value = queue.Dequeue();

            RouteNode next = node.Nexts.FirstOrDefault(p => string.Equals(p.Value, value));

            if (next == null)
            {
                next = new RouteNode();
                next.Value = value;
                next.Parent = node;

                node.Nexts.Add(next);
            }

            return _GetRoute(next, queue);
        }

        /// <summary>
        /// 获取路径队列
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>路径队列</returns>
        private Queue<string> GetPathQueue(string path)
        {
            string[] items = path.Split('/', StringSplitOptions.RemoveEmptyEntries);

            Queue<string> queue = new Queue<string>(items);

            return queue;
        }
    }
}
