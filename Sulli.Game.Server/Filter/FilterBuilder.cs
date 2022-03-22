using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 拦截器构建器
    /// </summary>
    public class FilterBuilder : IRouteBuilder
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

                // 全局拦截器
                this.AppendGlobalFilter(node, engine.Option);

                // 控制器拦截器
                this.AppendControllerFilter(node, engine.Option);

                // 方法拦截器
                this.AppendMethodFilter(node, engine.Option);

                return true;
            });
        }

        /// <summary>
        /// 添加全局拦截器
        /// </summary>
        /// <param name="node">当前路由节点</param>
        /// <param name="option">服务设置</param>
        private void AppendGlobalFilter(RouteNode node, ServerOption option)
        {
            foreach (IGlobalFilter filter in option.GlobalFilters)
            {
                if (string.IsNullOrWhiteSpace(filter.Regex))
                {
                    node.Filters.Add(filter);
                    continue;
                }

                Regex regex = new Regex(filter.Regex, RegexOptions.IgnoreCase);

                if (regex.IsMatch(node.ActionInfo.Uri.ToString()))
                {
                    node.Filters.Add(filter);
                }
            }
        }

        /// <summary>
        /// 添加控制器拦截器
        /// </summary>
        /// <param name="node">当前路由节点</param>
        /// <param name="option">服务设置</param>
        private void AppendControllerFilter(RouteNode node, ServerOption option)
        {
            object[] filters = node.ActionInfo.ControllerType.GetCustomAttributes(typeof(IFilter), false);

            foreach (object filter in filters)
            {
                node.Filters.Add(filter as IFilter);
            }
        }

        /// <summary>
        /// 添加方法拦截器
        /// </summary>
        /// <param name="node">当前路由节点</param>
        /// <param name="option">服务设置</param>
        private void AppendMethodFilter(RouteNode node, ServerOption option)
        {
            object[] filters = node.ActionInfo.MethodInfo.GetCustomAttributes(typeof(IFilter), false);

            foreach (object filter in filters)
            {
                node.Filters.Add(filter as IFilter);
            }
        }
    }
}
