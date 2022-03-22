using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 服务异常阶段
    /// </summary>
    public enum ServerExceptionStage
    {
        /// <summary>
        /// 服务构建
        /// </summary>
        Build,

        /// <summary>
        /// 服务启动
        /// </summary>
        Start,

        /// <summary>
        /// 查找Uri
        /// </summary>
        UriFound,

        /// <summary>
        /// 拦截器执行方法前拦截
        /// </summary>
        OnActionExecuting,

        /// <summary>
        /// 执行方法
        /// </summary>
        OnAction,

        /// <summary>
        /// 拦截器执行方法后拦截
        /// </summary>
        OnActionExecuted
    }
}
