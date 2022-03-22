using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 树辅助类
    /// </summary>
    /// <typeparam name="T">树节点类型</typeparam>
    public static class TreeHelper<T>
    {
        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="roots">根节点集合</param>
        /// <param name="getChildren">获取子节点方法</param>
        /// <param name="func">遍历方法</param>
        public static void Traverse(IList<T> roots, Func<T, IList<T>> getChildren, Func<T, bool> func)
        {
            if (roots == null || roots.Count == 0 || func == null)
                return;

            foreach (var node in roots)
            {
                if (!Traverse(node, getChildren, func))
                    return;
            }
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="node">遍历节点</param>
        /// <param name="getChildren">获取子节点方法</param>
        /// <param name="func">遍历方法</param>
        /// <returns>是否继续遍历</returns>
        public static bool Traverse(T node, Func<T, IList<T>> getChildren, Func<T, bool> func)
        {
            bool _continue = func(node);
            if (!_continue)
                return false;

            IList<T> children = getChildren(node);

            if (children == null || children.Count == 0)
                return true;

            foreach (T item in children)
            {
                if (!Traverse(item, getChildren, func))
                    return false;
            }

            return true;
        }
    }
}
