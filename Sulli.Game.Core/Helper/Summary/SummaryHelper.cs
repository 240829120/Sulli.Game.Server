using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 注释辅助类
    /// </summary>
    public class SummaryHelper
    {
        /// <summary>
        /// 缩进
        /// </summary>
        public const string INDENT_STRING = "\t";

        /// <summary>
        /// 程序集信息
        /// </summary>
        public List<SummaryAssemblyInfo> AssemblyInfos { get; private set; }

        /// <summary>
        /// 工作路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 注释辅助类
        /// </summary>
        /// <param name="path">工作路径</param>
        public SummaryHelper(string path)
        {
            this.Path = path;
        }

        /// <summary>
        /// 构建注释信息
        /// </summary>
        public void Build()
        {
            SummaryInfoBuilder builder = new SummaryInfoBuilder();
            this.AssemblyInfos = builder.Build(this.Path);
        }

        /// <summary>
        /// 获取类型注释信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>类型注释信息</returns>
        public SummaryTypeInfo GetTypeInfo(Type type)
        {
            SummaryAssemblyInfo assemblyInfo = this.AssemblyInfos.FirstOrDefault(p => IsSameAssembly(p.Assembly, type.Assembly));
            if (assemblyInfo == null)
                return null;

            return assemblyInfo.Types.FirstOrDefault(p => IsSameType(p.Type, type));
        }

        /// <summary>
        /// 获取方法信息
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        /// <returns>方法注释信息</returns>
        public SummaryMethodInfo GetMethodInfo(MethodInfo methodInfo)
        {
            SummaryTypeInfo typeInfo = this.GetTypeInfo(methodInfo.DeclaringType);
            if (typeInfo == null)
                return null;

            return this.GetMethodInfo(typeInfo, methodInfo);
        }

        /// <summary>
        /// 获取方法注释信息
        /// </summary>
        /// <param name="typeInfo">类型注释信息</param>
        /// <param name="methodInfo">方法信息</param>
        /// <returns>方法注释信息</returns>
        public SummaryMethodInfo GetMethodInfo(SummaryTypeInfo typeInfo, MethodInfo methodInfo)
        {
            return typeInfo.Methods.FirstOrDefault(p => IsSameMethod(p.MethodInfo, methodInfo));
        }

        /// <summary>
        /// 构建对象描述
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="indent">缩进</param>
        /// <param name="types">已经构建的类型</param>
        /// <returns>对象描述</returns>
        public string BuildObject(Type type, int indent, List<Type> types)
        {
            Type baseType = TypeHelper.GetBaseType(type);

            if (TypeHelper.IsPrimitive(baseType))
            {
                return $"[{baseType.Name}]";
            }
            else if (TypeHelper.IsDictionary(baseType))
            {
                return this.GetBuildDictionaryString(baseType, 0);
            }
            else if (TypeHelper.IsArrayOrList(baseType))
            {
                return this.GetBuildArrayOrListString(baseType, 0, types);
            }
            else
            {
                return this.GetBuildObjectString(baseType, indent, types);
            }
        }

        /// <summary>
        /// 获取对象描述字符串
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="indent">缩进</param>
        /// <param name="builedTypes">已经构建的类型集合</param>
        /// <returns>对象描述字符串</returns>
        private string GetBuildObjectString(Type type, int indent, List<Type> builedTypes)
        {
            Type baseType = TypeHelper.GetBaseType(type);

            if (builedTypes.Contains(baseType))
            {
                return this.GetBuildedObjectString(baseType, indent);
            }
            else
            {
                builedTypes.Add(baseType);
            }

            SummaryBuildContext context = new SummaryBuildContext();
            context.Type = baseType;
            context.BuiledTypes = builedTypes;
            context.SummaryTypeInfo = this.GetTypeInfo(context.Type);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.GetIndentString(indent)}{{");

            foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                context.PropertyInfo = propertyInfo;
                context.PropertyBaseType = TypeHelper.GetBaseType(propertyInfo.PropertyType);
                context.SummaryPropertyInfo = context.SummaryTypeInfo?.Propertys.FirstOrDefault(p => IsSameProperty(p.PropertyInfo, propertyInfo));

                sb.Append(this.GetBuildPropertyString(context, indent + 1));
            }

            sb.AppendLine($"{this.GetIndentString(indent)}}}");

            context.BuiledTypes.Add(context.Type);

            return sb.ToString();
        }

        /// <summary>
        /// 获取数组或列表描述字符串
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="indent">缩进</param>
        /// <param name="buildTypes">已经构建的类型集合</param>
        /// <returns>数组或列表描述字符串</returns>
        private string GetBuildArrayOrListString(Type type, int indent, List<Type> buildTypes)
        {
            Type itemType = type.IsArray ? type.GetElementType() : type.GetGenericArguments()[0];
            Type itemBaseType = TypeHelper.GetBaseType(itemType);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.GetIndentString(indent)}[");

            if (TypeHelper.IsPrimitive(itemBaseType))
            {
                sb.AppendLine($"{this.GetIndentString(indent + 1)}...... [{itemBaseType.Name}]");
            }
            else if (TypeHelper.IsDictionary(itemBaseType))
            {
                sb.Append(this.GetBuildDictionaryString(itemBaseType, indent + 1));
            }
            else if (TypeHelper.IsArrayOrList(itemBaseType))
            {
                sb.Append(this.GetBuildArrayOrListString(itemBaseType, indent + 1, buildTypes));
            }
            else
            {
                sb.Append(this.GetBuildObjectString(itemBaseType, indent + 1, buildTypes));
            }

            sb.AppendLine($"{this.GetIndentString(indent)}]");

            return sb.ToString();
        }

        /// <summary>
        /// 获取属性字符串
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="indent">缩进</param>
        /// <returns>属性描述字符串</returns>
        private string GetBuildPropertyString(SummaryBuildContext context, int indent)
        {
            StringBuilder sb = new StringBuilder();

            if (TypeHelper.IsPrimitive(context.PropertyBaseType))
            {
                sb.AppendLine($"{this.GetIndentString(indent)}{context.PropertyInfo.Name}: [{context.PropertyBaseType.Name}] {this.FormatSummary(context.SummaryPropertyInfo?.Summary)}");
            }
            else if (TypeHelper.IsDictionary(context.PropertyBaseType))
            {
                sb.AppendLine($"{this.GetIndentString(indent)}{context.PropertyInfo.Name}: {this.FormatSummary(context.SummaryPropertyInfo?.Summary)}");
                sb.Append(this.GetBuildDictionaryString(context.PropertyBaseType, indent));
            }
            else if (TypeHelper.IsArrayOrList(context.PropertyBaseType))
            {
                sb.AppendLine($"{this.GetIndentString(indent)}{context.PropertyInfo.Name}: {this.FormatSummary(context.SummaryPropertyInfo?.Summary)}");
                sb.Append(this.GetBuildArrayOrListString(context.PropertyBaseType, indent, context.BuiledTypes));
            }
            else
            {
                sb.AppendLine($"{this.GetIndentString(indent)}{context.PropertyInfo.Name}: [{context.PropertyBaseType.Name}] {this.FormatSummary(context.SummaryPropertyInfo?.Summary)}");
                sb.Append(this.GetBuildObjectString(context.PropertyBaseType, indent, context.BuiledTypes)); ;
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取字典描述字符串
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="indent">缩进</param>
        /// <returns>字典描述字符串</returns>
        private string GetBuildDictionaryString(Type type, int indent)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.GetIndentString(indent)}{{");
            sb.AppendLine($"{this.GetIndentString(indent + 1)}...... {{{{Dictionary<{TypeHelper.GetBaseType(type.GetGenericArguments()[0]).Name},{TypeHelper.GetBaseType(type.GetGenericArguments()[1]).Name}>}}}}");
            sb.AppendLine($"{this.GetIndentString(indent)}}}");

            return sb.ToString();
        }

        /// <summary>
        /// 获取已经构建过的对象字符串
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="indent">缩进</param>
        /// <returns>已经构建过的对象字符串</returns>
        private string GetBuildedObjectString(Type type, int indent)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.GetIndentString(indent)}{{");
            sb.AppendLine($"{this.GetIndentString(indent + 1)}...... {{{{{type.Name}}}}}");
            sb.AppendLine($"{this.GetIndentString(indent)}}}");

            return sb.ToString();
        }

        /// <summary>
        /// 获取缩进字符串
        /// </summary>
        /// <param name="indent">缩进</param>
        /// <returns>缩进字符串</returns>
        private string GetIndentString(int indent)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < indent; i++)
            {
                sb.Append(INDENT_STRING);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 格式化注释
        /// </summary>
        /// <param name="summary">注释</param>
        /// <returns>格式化后的注释</returns>
        private string FormatSummary(string summary)
        {
            if (string.IsNullOrWhiteSpace(summary))
                return string.Empty;

            return summary.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
        }



        /// <summary>
        /// 是否是同一个程序集
        /// </summary>
        /// <param name="a">程序集a</param>
        /// <param name="b">程序集b</param>
        /// <returns>是否是同一个程序集</returns>
        public static bool IsSameAssembly(Assembly a, Assembly b)
        {
            if (a == b)
                return true;

            return a.Location == b.Location;
        }

        /// <summary>
        /// 是否是同一个类型
        /// </summary>
        /// <param name="a">类型A</param>
        /// <param name="b">类型B</param>
        /// <returns>是否是同一个类型</returns>
        public static bool IsSameType(Type a, Type b)
        {
            if (a == b)
                return true;

            if (!IsSameAssembly(a.Assembly, b.Assembly))
                return false;

            return a.FullName == b.FullName;
        }

        /// <summary>
        /// 是否是同一个属性
        /// </summary>
        /// <param name="a">属性a</param>
        /// <param name="b">属性b</param>
        /// <returns>是否是同一个属性</returns>
        public static bool IsSameProperty(PropertyInfo a, PropertyInfo b)
        {
            if (a == b)
                return true;

            if (!IsSameType(a.PropertyType, b.PropertyType))
                return false;

            return a.Name == b.Name && IsSameType(a.DeclaringType, b.DeclaringType);
        }

        /// <summary>
        /// 是否是同一个方法
        /// </summary>
        /// <param name="a">方法a</param>
        /// <param name="b">方法b</param>
        /// <returns>是否是同一个方法</returns>
        public static bool IsSameMethod(MethodInfo a, MethodInfo b)
        {
            if (a == b)
                return true;

            if (!IsSameType(a.DeclaringType, b.DeclaringType))
                return false;

            return a.ToString() == b.ToString();
        }

        /// <summary>
        /// 是否是同一个参数
        /// </summary>
        /// <param name="a">参数a</param>
        /// <param name="b">参数b</param>
        /// <returns>是否是同一个参数</returns>
        public static bool IsSameParameter(ParameterInfo a, ParameterInfo b)
        {
            if (a == b)
                return true;

            if (!IsSameType(a.ParameterType, b.ParameterType))
                return false;

            return a.ToString() == b.ToString();
        }
    }
}
