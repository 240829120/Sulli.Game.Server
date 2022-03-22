using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 路由节点
    /// </summary>
    public class RouteNode
    {
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 是否是根节点
        /// </summary>
        public bool IsRoot { get; set; }

        /// <summary>
        /// 是否是叶子节点
        /// </summary>
        public bool IsLeaf { get; set; }

        /// <summary>
        /// 方法信息
        /// </summary>
        public ActionInfo ActionInfo { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public RouteNode Parent { get; set; }

        /// <summary>
        /// 路由数据
        /// </summary>
        public Dictionary<string, object> Data { get; private set; } = new Dictionary<string, object>();

        /// <summary>
        /// 拦截器集合
        /// </summary>
        public List<IFilter> Filters { get; private set; } = new List<IFilter>();

        /// <summary>
        /// 子节点
        /// </summary>
        public List<RouteNode> Nexts { get; private set; } = new List<RouteNode>();
    }
}
