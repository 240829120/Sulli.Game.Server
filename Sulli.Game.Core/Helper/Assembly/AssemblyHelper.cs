using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Features.AttributeFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 程序集辅助类
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// 程序集
        /// </summary>
        public static IReadOnlyList<Assembly>? Assemblies { get; private set; }

        /// <summary>
        /// IOC参数构建器
        /// </summary>
        public static List<IAssemblyIocParameterBuildProvider> IocParameterBuildProviders { get; private set; } = new List<IAssemblyIocParameterBuildProvider>();

        /// <summary>
        /// 程序集辅助类
        /// </summary>
        static AssemblyHelper()
        {
            InitAssembly();
            InitIoc();
        }

        /// <summary>
        /// 初始化程序集
        /// </summary>
        private static void InitAssembly()
        {
            List<Assembly> list = new List<Assembly>();

            string[] files = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");

            Assembly[] localAssemblys = AppDomain.CurrentDomain.GetAssemblies();

            foreach (string file in files)
            {
                if (file.Contains("Microsoft"))
                    continue;

                Assembly assembly = Assembly.LoadFrom(file);
                Assembly? local = localAssemblys.FirstOrDefault(p => string.Equals(p.FullName, assembly.FullName));

                if (local == null)
                {
                    local = AppDomain.CurrentDomain.Load(assembly.GetName());
                }

                list.Add(local);
            }

            Assemblies = list;
        }

        /// <summary>
        /// 初始化IOC
        /// </summary>
        private static void InitIoc()
        {
            IocParameterBuildProviders.Add(new LogIocParameterBuildProvider());
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="action">行为</param>
        public static void Foreach(Action<Assembly, Type> action)
        {
            foreach (Assembly assembly in Assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    action(assembly, type);
                }
            }
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="func">程序集</param>
        /// <param name="action">类型</param>
        public static void Foreach(Func<Assembly, bool> func, Action<Type> action)
        {
            foreach (Assembly assembly in Assemblies)
            {
                if (!func(assembly))
                    continue;

                foreach (Type type in assembly.GetTypes())
                {
                    action(type);
                }
            }
        }

        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <param name="assembly">程序集名称</param>
        /// <returns>程序集</returns>
        public static Assembly? GetAssembly(string assembly)
        {
            return Assemblies.FirstOrDefault(p => p.GetName().Name == assembly || p.FullName == assembly);
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="assembly">程序集名称</param>
        /// <param name="type">类型</param>
        /// <returns>类型</returns>
        public static Type? GetType(Assembly assembly, string type)
        {
            return assembly.GetTypes().FirstOrDefault(p => p.Name == type || p.FullName == type);
        }

        /// <summary>
        /// 使用IOC根据参数构建单例
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="asType">转化类型</param>
        /// <param name="builder">构建器</param>
        public static IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> IocBuildWithParameters(Type type, Type asType, ContainerBuilder builder)
        {
            List<Parameter> parameters = new List<Parameter>();
            ConstructorInfo[] constructors = type.GetConstructors();
            if (constructors.Length > 1)
            {
                throw new Exception($"type: {type.FullName} ,have more constructor.");
            }

            ConstructorInfo constructor = constructors[0];

            foreach (ParameterInfo parameter in constructor.GetParameters())
            {
                Parameter item = new ResolvedParameter((pi, ctx) => pi.ParameterType == parameter.ParameterType && pi.Name == parameter.Name, IocBuildWithParametersValueAccessor);

                parameters.Add(item);
            }

            return builder.RegisterType(type).As(asType).WithParameters(parameters);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="parameterInfo">参数信息</param>
        /// <param name="context">IOC上下文</param>
        /// <returns>值</returns>
        private static object IocBuildWithParametersValueAccessor(ParameterInfo parameterInfo, IComponentContext context)
        {
            foreach (IAssemblyIocParameterBuildProvider provider in IocParameterBuildProviders)
            {
                if (provider.CanValueAccessor(parameterInfo, context))
                {
                    return provider.ValueAccessor(parameterInfo, context);
                }
            }

            KeyFilterAttribute? keyFilter = parameterInfo.GetCustomAttribute<KeyFilterAttribute>(false);
            if (keyFilter != null)
            {
                return context.ResolveKeyed(keyFilter.Key, parameterInfo.ParameterType);
            }

            return context.Resolve(parameterInfo.ParameterType);
        }
    }
}
