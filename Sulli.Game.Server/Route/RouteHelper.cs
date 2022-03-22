using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 路由辅助类
    /// </summary>
    public static class RouteHelper
    {
        /// <summary>
        /// 分割符匹配表达式
        /// </summary>
        private static Regex Splite_Regex = new Regex(@"^[/\\]+$");

        /// <summary>
        /// 路径格式化字符串
        /// </summary>
        private static Regex PathFormat_Regex = new Regex(@"[/\\]+");

        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="roots">根节点集合</param>
        /// <param name="func">遍历方法</param>
        public static void Traverse(Dictionary<ActionType, RouteNode> roots, Func<RouteNode, bool> func)
        {
            if (roots == null || roots.Count == 0 || func == null)
                return;

            foreach (var kv in roots)
            {
                if (!Traverse(kv.Value, func))
                    return;
            }
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="node">遍历节点</param>
        /// <param name="func">遍历方法</param>
        /// <returns>是否继续遍历</returns>
        private static bool Traverse(RouteNode node, Func<RouteNode, bool> func)
        {
            bool _continue = func(node);
            if (!_continue)
                return false;

            if (node.Nexts == null || node.Nexts.Count == 0)
                return true;

            foreach (RouteNode item in node.Nexts)
            {
                if (!Traverse(item, func))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="roots">根节点集合</param>
        /// <param name="uri">路径</param>
        /// <returns>节点</returns>
        public static RouteNode Find(Dictionary<ActionType, RouteNode> roots, Uri uri)
        {
            string scheme = uri.Scheme;
            ActionType actionType = GetActionType(uri.Scheme);

            RouteNode node = roots[actionType];
            Queue<string> segments = new Queue<string>(uri.Segments.Where(p => !Splite_Regex.IsMatch(p)).Select(p => p.Replace("/", String.Empty).Replace(@"\", string.Empty)).ToList());

            return Find(node, segments);
        }

        /// <summary>
        /// 获取方法类型
        /// </summary>
        /// <param name="scheme">协议</param>
        /// <returns>方法类型</returns>
        public static ActionType GetActionType(string scheme)
        {
            scheme = scheme.Trim().ToLower();

            switch (scheme)
            {
                case "http":
                case "https":
                    return ActionType.WebApi;

                case "ws":
                case "wss":
                    return ActionType.WebSocket;
                default:
                    break;
            }

            return ActionType.None;
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="segments">参数</param>
        /// <returns>查找到的节点</returns>
        private static RouteNode Find(RouteNode node, Queue<string> segments)
        {
            if (node == null || segments.Count == 0)
                return node;

            string segment = segments.Dequeue();

            return Find(node.Nexts.FirstOrDefault(p => string.Equals(p.Value, segment)), segments);
        }

        /// <summary>
        /// 格式化路径
        /// </summary>
        /// <param name="path1">路径1</param>
        /// <param name="path2">路径2</param>
        /// <returns>格式化后的路径</returns>
        public static string FormatPath(string path1, string path2)
        {
            string path = PathFormat_Regex.Replace($"{path1}/{path2}", "/").Trim();
            if (path.StartsWith("/"))
                path = path[1..];
            if (path.EndsWith("/"))
                path = path[0..^1];

            return path;
        }
    }
}
