using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Sulli.Game.Core;
using System.Diagnostics;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 方法加载器
    /// </summary>
    public class ActionLoader
    {
        /// <summary>
        /// 日志
        /// </summary>
        private static ILog logger = LogManager.GetLogger(typeof(ActionLoader));

        /// <summary>
        /// 方法加载器
        /// </summary>
        /// <param name="option">服务设置</param>
        public ActionLoader(ServerOption option)
        {
            this.Option = option;
        }

        /// <summary>
        /// 服务设置
        /// </summary>
        public ServerOption Option { get; private set; }

        /// <summary>
        /// 服务集合
        /// </summary>
        public List<ActionInfo> Actions { get; private set; } = new List<ActionInfo>();

        /// <summary>
        /// 加载服务信息
        /// </summary>
        public void LoadServiceInfo()
        {
            AssemblyHelper.Foreach(
                assembly => this.Option.ControllerRegisterContainer.Has(assembly),
                type =>
                {
                    ControllerAttribute controllerAttribute = type.GetCustomAttribute<ControllerAttribute>(false);
                    RouteAttribute controllerRouteAttribute = type.GetCustomAttribute<RouteAttribute>(false);
                    if (controllerAttribute == null)
                        return;

                    AssemblyRegisterInfo registerInfo = this.Option.ControllerRegisterContainer.Get(type.Assembly);
                    if (!registerInfo.CanRegister(type))
                        return;

                    this.SearchType(type, controllerAttribute, controllerRouteAttribute);

                    Debug.WriteLine($"====> Load Controller, Assembly: {type.Assembly.GetName().Name}, Type: {type.FullName}");
                });
        }

        /// <summary>
        /// 搜索类型
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="controllerAttribute">服务类属性</param>
        /// <param name="controllerRouteAttribute">控制器路由属性</param>
        private void SearchType(Type type, ControllerAttribute controllerAttribute, RouteAttribute controllerRouteAttribute)
        {
            foreach (MethodInfo method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public))
            {
                RouteAttribute routeAttribute = method.GetCustomAttribute<RouteAttribute>(false);
                if (routeAttribute == null)
                    continue;

                IEnumerable<ActionBaseAttribute> actionAttributes = method.GetCustomAttributes<ActionBaseAttribute>(false);
                if (actionAttributes == null)
                    continue;

                DescriptionAttribute descriptionAttribute = method.GetCustomAttribute<DescriptionAttribute>(false);

                foreach (ActionBaseAttribute item in actionAttributes)
                {
                    ActionInfo info = new ActionInfo();
                    info.Type = item.Type;
                    info.Uri = new Uri($"{item.Type}:///{RouteHelper.FormatPath(controllerRouteAttribute.Path, routeAttribute.Path)}");
                    info.ControllerRoute = controllerRouteAttribute;
                    info.ActionRoute = routeAttribute;
                    info.Description = descriptionAttribute;
                    info.ControllerType = type;
                    info.MethodInfo = method;
                    info.Methods.AddRange(item.GetMethods());

                    this.FullParameter(info);

                    this.Actions.Add(info);
                }
            }
        }

        /// <summary>
        /// 填充参数信息
        /// </summary>
        /// <param name="actionInfo">方法信息</param>
        private void FullParameter(ActionInfo actionInfo)
        {
            foreach (ParameterInfo parameterInfo in actionInfo.MethodInfo.GetParameters())
            {
                ActionParameterInfo info = new ActionParameterInfo();
                info.ParameterInfo = parameterInfo;
                info.FromAttribute = parameterInfo.GetCustomAttribute<FromBaseAttribute>(false);
                if (info.FromAttribute == null)
                {
                    info.FromAttribute = new FromBodyItemAttribute();
                }
                info.Name = string.IsNullOrWhiteSpace(info.FromAttribute.Name) ? parameterInfo.Name : info.FromAttribute.Name;

                actionInfo.ParameterInfos.Add(info);
            }
        }
    }
}
