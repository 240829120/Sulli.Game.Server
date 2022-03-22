using Autofac;
using Sulli.Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 服务设置
    /// </summary>
    public class ServerOption
    {
        /// <summary>
        /// 全局拦截器
        /// </summary>
        public List<IGlobalFilter> GlobalFilters { get; private set; } = new List<IGlobalFilter>();

        /// <summary>
        /// 连接器集合
        /// </summary>
        public List<IConnection> Connections { get; private set; } = new List<IConnection>();

        /// <summary>
        /// 控制器程序集注册信息
        /// </summary>
        public AssemblyRegisterContainer ControllerRegisterContainer { get; set; } = new AssemblyRegisterContainer();
    }
}
