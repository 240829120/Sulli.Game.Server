using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 控制器
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// 请求参数
        /// </summary>
        RequestContext RequestContext { get; set; }

        /// <summary>
        /// 拦截器上下文
        /// </summary>
        FilterContext FilterContext { get; set; }

        /// <summary>
        /// 对象生命周期
        /// </summary>
        ILifetimeScope LifetimeScope { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        Task Initialize();
    }
}
