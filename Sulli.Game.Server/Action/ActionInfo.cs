using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 方法信息
    /// </summary>
    public class ActionInfo
    {
        /// <summary>
        /// 控制器路由信息
        /// </summary>
        public RouteAttribute ControllerRoute { get; set; }

        /// <summary>
        /// 方法路由信息
        /// </summary>
        public RouteAttribute ActionRoute { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public DescriptionAttribute Description { get; set; }

        /// <summary>
        /// 完整URI
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// 方法类型
        /// </summary>
        public ActionType Type { get; set; }

        /// <summary>
        /// 控制器类型
        /// </summary>
        public Type ControllerType { get; set; }

        /// <summary>
        /// 方法信息
        /// </summary>
        public MethodInfo MethodInfo { get; set; }

        /// <summary>
        /// 方法协议支持
        /// </summary>
        public List<string> Methods { get; set; } = new List<string>();

        /// <summary>
        /// 参数信息
        /// </summary>
        public List<ActionParameterInfo> ParameterInfos { get; set; } = new List<ActionParameterInfo>();

        /// <summary>
        /// 数据
        /// </summary>
        public Dictionary<string, object> Data { get; private set; } = new Dictionary<string, object>();
    }
}
