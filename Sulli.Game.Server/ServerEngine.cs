using Autofac;
using Sulli.Game.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 服务引擎
    /// </summary>
    public class ServerEngine
    {
        /// <summary>
        /// 服务设置
        /// </summary>
        public ServerOption Option { get; private set; }

        /// <summary>
        /// 路由构建器
        /// </summary>
        public List<IRouteBuilder> RouteBuilders { get; private set; } = new List<IRouteBuilder>();

        /// <summary>
        /// 路由根集合
        /// </summary>
        public Dictionary<ActionType, RouteNode>? Roots { get; private set; }

        /// <summary>
        /// 连接器集合
        /// </summary>
        public List<IConnection> Connections { get; private set; } = new List<IConnection>();

        /// <summary>
        /// 异常时触发
        /// </summary>
        public event EventHandler<ServerExceptionEventArgs>? OnException;

        /// <summary>
        /// 路径未找到时触发
        /// </summary>
        public event EventHandler<ServerExceptionEventArgs>? OnUriNotFound;

        /// <summary>
        /// 服务引擎
        /// </summary>
        /// <param name="option">服务配置</param>
        public ServerEngine(ServerOption option)
        {
            this.Option = option;
        }

        /// <summary>
        /// 启动引擎
        /// </summary>
        public async Task StartAsync()
        {
            await Task.Run(() =>
            {
                foreach (IConnection connection in this.Connections)
                {
                    connection.Start();
                }
            });
        }

        /// <summary>
        /// 构建服务
        /// </summary>
        public async Task BuildAsync()
        {
            await Task.Run(() =>
            {
                // 加载方法信息
                ActionLoader actionLoader = new ActionLoader(this.Option);
                actionLoader.LoadServiceInfo();

                // 构建路由信息
                RouteLoader routeLoader = new RouteLoader(this);
                routeLoader.Build(actionLoader.Actions);
                this.Roots = routeLoader.Roots;
            });
        }

        /// <summary>
        /// 调用方法
        /// </summary>
        /// <param name="requestContext">请求上下文</param>
        /// <param name="responseContext">返回上下文</param>
        /// <returns>是否正确执行完成</returns>
        public async Task CallAsync(RequestContext requestContext, ResponseContext responseContext)
        {
            if (this.Roots == null || Application.Current == null || Application.Current.Container == null)
                return;

            RouteNode node = RouteHelper.Find(this.Roots, requestContext.Uri);

            if (node == null || (node.ActionInfo.Methods.Count != 0 && !node.ActionInfo.Methods.Contains(requestContext.Method)))
            {
                ServerException exception = new ServerException($"execute find uri error, uri: {requestContext.Uri}");
                exception.Engine = this;
                exception.RouteNode = node;
                exception.Uri = requestContext.Uri;
                exception.RequestContext = requestContext;
                exception.ResponseContext = responseContext;
                exception.Stage = ServerExceptionStage.UriFound;

                this.ExecuteOnUriNotFound(exception);

                return;
            }

            using (ILifetimeScope scope = Application.Current.Container.BeginLifetimeScope())
            {
                object instance = scope.Resolve(node.ActionInfo.ControllerType);
                JObject request = string.IsNullOrWhiteSpace(requestContext.Body) ? new JObject() : JObject.Parse(requestContext.Body);
                object[] parameters = GetParameters(requestContext, node, request);

                FilterContext filterContext = new FilterContext(this, node.ActionInfo, node.Filters);

                // 拦截器 -- 方法执行前
                if (!await this.CallBefore(node, requestContext, responseContext, filterContext, scope))
                    return;

                // 控制器 -- 执行
                await this.FullIController(requestContext, filterContext, scope, instance);
                if (!await this.CallAsync(node, requestContext, responseContext, instance, parameters))
                    return;

                // 拦截器 -- 方法执行后
                if (!await this.CallAfter(node, requestContext, responseContext, filterContext, scope))
                    return;
            }
        }

        /// <summary>
        /// 调用方法前执行
        /// </summary>
        /// <param name="node">路由节点</param>
        /// <param name="requestContext">请求上下文</param>
        /// <param name="filterContext">拦截器上下文</param>
        /// <param name="scope">对象生命周期</param>
        /// <returns>是否继续执行</returns>
        private async Task<bool> CallBefore(RouteNode node, RequestContext requestContext, ResponseContext responseContext, FilterContext filterContext, ILifetimeScope scope)
        {
            try
            {
                for (int i = 0; i < node.Filters.Count; i++)
                {
                    IFilter filter = node.Filters[i];

                    await filter.OnActionExecuting(requestContext, responseContext, filterContext, scope);
                    if (!filterContext.IsContinue)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ServerException exception = new ServerException($"execute filter OnActionExecuting error, uri: {node.ActionInfo.Uri}", ex);
                exception.Engine = this;
                exception.RouteNode = node;
                exception.Uri = requestContext.Uri;
                exception.RequestContext = requestContext;
                exception.ResponseContext = responseContext;
                exception.FilterContext = filterContext;
                exception.Stage = ServerExceptionStage.OnActionExecuting;

                this.ExecuteOnException(exception);

                return false;
            }
        }

        /// <summary>
        /// 调用方法后执行
        /// </summary>
        /// <param name="node">路由节点</param>
        /// <param name="requestContext">请求上下文</param>
        /// <param name="responseContext">返回上下文</param>
        /// <param name="filterContext">拦截器上下文</param>
        /// <param name="scope">对象生命周期</param>
        /// <returns>是否继续执行</returns>
        private async Task<bool> CallAfter(RouteNode node, RequestContext requestContext, ResponseContext responseContext, FilterContext filterContext, ILifetimeScope scope)
        {
            try
            {
                for (int i = node.Filters.Count - 1; i >= 0; i--)
                {
                    IFilter filter = node.Filters[i];

                    await filter.OnActionExecuted(requestContext, responseContext, filterContext, scope);
                    if (!filterContext.IsContinue)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ServerException exception = new ServerException($"execute filter OnActionExecuted error, uri: {node.ActionInfo.Uri}", ex);
                exception.Engine = this;
                exception.RouteNode = node;
                exception.Uri = requestContext.Uri;
                exception.RequestContext = requestContext;
                exception.ResponseContext = responseContext;
                exception.FilterContext = filterContext;
                exception.Stage = ServerExceptionStage.OnActionExecuted;

                this.ExecuteOnException(exception);

                return false;
            }
        }

        /// <summary>
        /// 调用方法
        /// </summary>
        /// <param name="node">路由节点</param>
        /// <param name="requestContext">请求上下文</param>
        /// <param name="responseContext">返回上下文</param>
        /// <param name="instance">控制器实例</param>
        /// <param name="parameters">方法参数</param>
        private async Task<bool> CallAsync(RouteNode node, RequestContext requestContext, ResponseContext responseContext, object instance, object[] parameters)
        {
            try
            {
                // 控制器执行
                object result = node.ActionInfo.MethodInfo.Invoke(instance, parameters);

                object value = null;

                if (result is Task)
                {
                    dynamic task = (Task)result;

                    await task;

                    value = task.Result;
                }
                else
                {
                    value = result;
                }

                if (value is IResult)
                {
                    responseContext.Result = value as IResult;
                }
                else
                {
                    responseContext.Result = new JsonResult(value);
                }

                return true;
            }
            catch (Exception ex)
            {
                ServerException exception = new ServerException($"execute controller method error, uri: {node.ActionInfo.Uri}", ex);
                exception.Engine = this;
                exception.RouteNode = node;
                exception.Uri = requestContext.Uri;
                exception.RequestContext = requestContext;
                exception.ResponseContext = responseContext;
                exception.Stage = ServerExceptionStage.OnAction;

                this.ExecuteOnException(exception);

                responseContext.Exception = exception;

                return false;
            }
        }

        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <param name="node">路由节点</param>
        /// <param name="request">请求对象</param>
        /// <returns>参数列表</returns>
        private object[] GetParameters(RequestContext context, RouteNode node, JObject request)
        {
            object[] parameters = new object[node.ActionInfo.ParameterInfos.Count];

            for (int i = 0; i < node.ActionInfo.ParameterInfos.Count; i++)
            {
                ActionParameterInfo info = node.ActionInfo.ParameterInfos[i];

                switch (info.FromAttribute.Type)
                {
                    case FromType.FromBody:
                        parameters[i] = request.ToObject(info.ParameterInfo.ParameterType);
                        break;
                    case FromType.FromBodyItem:
                        parameters[i] = request.Property(info.Name)?.Value.ToObject(info.ParameterInfo.ParameterType);
                        break;
                    case FromType.FromHeader:
                        parameters[i] = context.Header.ContainsKey(info.Name) ? context.Header[info.Name] : (info.ParameterInfo.ParameterType.IsValueType ? Activator.CreateInstance(info.ParameterInfo.ParameterType) : null);
                        break;
                    case FromType.FromQuery:

                        break;
                    default:
                        break;
                }
            }

            return parameters;
        }

        /// <summary>
        /// 填充控制器接口
        /// </summary>
        /// <param name="requestContext">请求上下文</param>
        /// <param name="filterContext">拦截上下文</param>
        /// <param name="lifetimeScope">生命周期</param>
        /// <param name="instance">控制器实例</param>
        private async Task FullIController(RequestContext requestContext, FilterContext filterContext, ILifetimeScope lifetimeScope, object instance)
        {
            IController controller = instance as IController;
            if (controller == null)
                return;

            controller.RequestContext = requestContext;
            controller.FilterContext = filterContext;
            controller.LifetimeScope = lifetimeScope;

            await controller.Initialize();
        }

        /// <summary>
        /// 执行异常处理
        /// </summary>
        /// <param name="exception">异常</param>
        internal void ExecuteOnException(ServerException exception)
        {
            if (this.OnException == null)
                return;

            ServerExceptionEventArgs args = new ServerExceptionEventArgs();
            args.Engine = this;
            args.Exception = exception;

            this.OnException.Invoke(this, args);
        }

        /// <summary>
        /// 执行URI未找到
        /// </summary>
        /// <param name="exception">异常</param>
        internal void ExecuteOnUriNotFound(ServerException exception)
        {
            if (this.OnUriNotFound == null)
                return;

            ServerExceptionEventArgs args = new ServerExceptionEventArgs();
            args.Engine = this;
            args.Exception = exception;

            this.OnUriNotFound.Invoke(this, args);
        }

    }
}
