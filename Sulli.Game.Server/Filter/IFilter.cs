using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 拦截器
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// 方法执行前拦截
        /// </summary>
        /// <param name="requestContext">请求上下文</param>
        /// <param name="responseContext">返回上下文</param>
        /// <param name="filterContent">拦截器上下文</param>
        /// <param name="lifetimeScope">对象生命周期</param>
        Task OnActionExecuting(RequestContext requestContext, ResponseContext responseContext, FilterContext filterContent, ILifetimeScope lifetimeScope);

        /// <summary>
        /// 方法执行后拦截
        /// </summary>
        /// <param name="requestContext">请求上下文</param>
        /// <param name="responseContext">返回上下文</param>
        /// <param name="filterContent">拦截器上下文</param>
        /// <param name="scope">对象生命周期</param>
        Task OnActionExecuted(RequestContext requestContext, ResponseContext responseContext, FilterContext filterContent, ILifetimeScope lifetimeScope);
    }
}
