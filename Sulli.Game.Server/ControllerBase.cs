using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public abstract class ControllerBase : IController
    {
        /// <summary>
        /// 请求参数
        /// </summary>
        public RequestContext RequestContext { get; set; }

        /// <summary>
        /// 拦截器上下文
        /// </summary>
        public FilterContext FilterContext { get; set; }

        /// <summary>
        /// 对象生命周期
        /// </summary>
        public ILifetimeScope LifetimeScope { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual async Task Initialize()
        {
            await Task.CompletedTask;
        }
    }
}
