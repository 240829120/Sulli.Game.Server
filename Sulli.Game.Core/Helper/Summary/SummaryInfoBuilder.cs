using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 注释信息构建器
    /// </summary>
    public class SummaryInfoBuilder
    {
        /// <summary>
        /// 构建注释信息
        /// </summary>
        /// <param name="path">工作路径</param>
        /// <returns>注释信息</returns>
        public List<SummaryAssemblyInfo> Build(string path)
        {
            List<SummaryAssemblyInfo> result = new List<SummaryAssemblyInfo>();

            SummaryReader reader = new SummaryReader();
            List<SummaryDocNode> docs = reader.Load(path);
            foreach (SummaryDocNode doc in docs)
            {
                SummaryAssemblyInfo info = this.BuildAssembly(doc);

                result.Add(info);
            }

            foreach (SummaryAssemblyInfo assemblyInfo in result)
            {
                this.FullAssemblyInfo(result, assemblyInfo);
            }

            return result;
        }

        /// <summary>
        /// 构建程序集信息
        /// </summary>
        /// <param name="doc">Doc节点</param>
        /// <returns>程序集信息</returns>
        private SummaryAssemblyInfo BuildAssembly(SummaryDocNode doc)
        {
            SummaryAssemblyInfo assemblyInfo = new SummaryAssemblyInfo();
            assemblyInfo.Name = doc.Assembly.Name;
            assemblyInfo.Assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(p => p.GetName().Name == assemblyInfo.Name);

            for (int offset = 0; offset < doc.Members.Count; ++offset)
            {
                SummaryMemberNode node = doc.Members[offset];

                switch (node.Type)
                {
                    case SummaryMemberType.T:
                        this.BuildTypeInfo(doc, node, assemblyInfo);
                        break;
                    case SummaryMemberType.F:
                        this.BuildFieldInfo(doc, node, assemblyInfo);
                        break;
                    case SummaryMemberType.M:
                        this.BuildMethodInfo(doc, node, assemblyInfo);
                        break;
                    case SummaryMemberType.P:
                        this.BuildPropertyInfo(doc, node, assemblyInfo);
                        break;
                    default:
                        break;
                }
            }

            return assemblyInfo;
        }

        /// <summary>
        /// 构建类型信息
        /// </summary>
        /// <param name="doc">Doc节点</param>
        /// <param name="node">Member节点</param>
        /// <param name="assemblyInfo">程序集信息</param>
        private void BuildTypeInfo(SummaryDocNode doc, SummaryMemberNode node, SummaryAssemblyInfo assemblyInfo)
        {
            SummaryTypeInfo typeInfo = new SummaryTypeInfo();

            typeInfo.FullName = $"{node.Name}, {assemblyInfo.Name}";
            typeInfo.Name = node.Name.Split('.').Last().Split('`')[0];
            typeInfo.Summary = node.Summary;
            typeInfo.Type = Type.GetType(typeInfo.FullName);

            assemblyInfo.Types.Add(typeInfo);
        }

        /// <summary>
        /// 构建属性信息
        /// </summary>
        /// <param name="doc">Doc节点</param>
        /// <param name="node">Member节点</param>
        /// <param name="assemblyInfo">程序集信息</param>
        private void BuildPropertyInfo(SummaryDocNode doc, SummaryMemberNode node, SummaryAssemblyInfo assemblyInfo)
        {
            SummaryTypeInfo typeInfo = assemblyInfo.Types.Last();

            SummaryPropertyInfo propertyInfo = new SummaryPropertyInfo();
            propertyInfo.Name = node.Name.Split('.').Last().Split('`')[0];
            propertyInfo.Summary = node.Summary;
            propertyInfo.PropertyInfo = typeInfo.Type.GetProperty(propertyInfo.Name);

            typeInfo.Propertys.Add(propertyInfo);
        }

        /// <summary>
        /// 构建字段信息
        /// </summary>
        /// <param name="doc">Doc节点</param>
        /// <param name="node">Member节点</param>
        /// <param name="assemblyInfo">程序集信息</param>
        private void BuildFieldInfo(SummaryDocNode doc, SummaryMemberNode node, SummaryAssemblyInfo assemblyInfo)
        {
            SummaryTypeInfo typeInfo = assemblyInfo.Types.Last();

            SummaryFieldInfo fieldInfo = new SummaryFieldInfo();
            fieldInfo.Name = node.Name.Split('.').Last().Split('`')[0];
            fieldInfo.Summary = node.Summary;
            fieldInfo.FieldInfo = typeInfo.Type.GetField(fieldInfo.Name);

            typeInfo.Fields.Add(fieldInfo);
        }

        /// <summary>
        /// 构建字段信息
        /// </summary>
        /// <param name="doc">Doc节点</param>
        /// <param name="node">Member节点</param>
        /// <param name="assemblyInfo">程序集信息</param>
        private void BuildMethodInfo(SummaryDocNode doc, SummaryMemberNode node, SummaryAssemblyInfo assemblyInfo)
        {
            SummaryTypeInfo typeInfo = assemblyInfo.Types.Last();

            SummaryMethodInfo methodInfo = new SummaryMethodInfo();
            string[] parts = node.Name.Split(new char[] { '(', '`', ')' }, StringSplitOptions.RemoveEmptyEntries);
            string[] args = parts.Length > 1 ? parts[1].Split(',') : new string[0];
            methodInfo.Name = parts[0].Split('.').Last();
            methodInfo.Summary = node.Summary;
            methodInfo.Returns = node.Returns;

            List<MethodInfo> methods = typeInfo.Type.GetMethods().Where(p => p.Name == methodInfo.Name).ToList();
            foreach (MethodInfo method in methods)
            {
                if (!this.IsSameMethod(method, args))
                    continue;

                methodInfo.MethodInfo = method;

                foreach (ParameterInfo parameter in method.GetParameters())
                {
                    SummaryParamInfo parameterInfo = new SummaryParamInfo();
                    parameterInfo.Name = parameter.Name;
                    parameterInfo.Summary = node.Params.FirstOrDefault(p => p.Name == parameter.Name)?.Content;
                    parameterInfo.ParameterInfo = parameter;

                    methodInfo.Params.Add(parameterInfo);
                }

                break;
            }

            typeInfo.Methods.Add(methodInfo);
        }

        /// <summary>
        /// 填充程序集信息
        /// </summary>
        /// <param name="assemblyInfos">程序集信息集合</param>
        /// <param name="assemblyInfo">程序集信息</param>
        private void FullAssemblyInfo(List<SummaryAssemblyInfo> assemblyInfos, SummaryAssemblyInfo assemblyInfo)
        {
            foreach (SummaryTypeInfo typeInfo in assemblyInfo.Types)
            {
                this.FullTypeInfo(assemblyInfos, assemblyInfo, typeInfo);
            }
        }

        /// <summary>
        /// 填充程序集类型信息
        /// </summary>
        /// <param name="assemblyInfos">程序集信息集合</param>
        /// <param name="assemblyInfo">程序集信息</param>
        /// <param name="typeInfo">类型信息</param>
        private void FullTypeInfo(List<SummaryAssemblyInfo> assemblyInfos, SummaryAssemblyInfo assemblyInfo, SummaryTypeInfo typeInfo)
        {
            foreach (PropertyInfo property in typeInfo.Type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (property.DeclaringType == typeInfo.Type)
                    continue;

                SummaryTypeInfo declaringTypeInfo = this.GetSummaryTypeInfo(assemblyInfos, property.DeclaringType);
                if (declaringTypeInfo == null)
                    continue;

                SummaryPropertyInfo propertyInfo = declaringTypeInfo.Propertys.FirstOrDefault(p => SummaryHelper.IsSameProperty(p.PropertyInfo, property));
                if (propertyInfo == null)
                    continue;

                typeInfo.Propertys.Add(propertyInfo);
            }
        }

        /// <summary>
        /// 是否是同一个方法
        /// </summary>
        /// <param name="method">方法</param>
        /// <param name="args">参数信息</param>
        /// <returns>是否是同一个方法</returns>
        private bool IsSameMethod(MethodInfo method, string[] args)
        {
            ParameterInfo[] parameters = method.GetParameters();
            if (parameters.Length != args.Length)
                return false;

            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo parameter = parameters[i];
                string arg = args[i];

                if (parameter.ParameterType.FullName != arg)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 获取类型注释信息
        /// </summary>
        /// <param name="assemblyInfos">程序集信息</param>
        /// <param name="type">类型</param>
        /// <returns>类型注释信息</returns>
        private SummaryTypeInfo GetSummaryTypeInfo(List<SummaryAssemblyInfo> assemblyInfos, Type type)
        {
            SummaryAssemblyInfo assemblyInfo = assemblyInfos.FirstOrDefault(p => SummaryHelper.IsSameAssembly(p.Assembly, type.Assembly));
            if (assemblyInfo == null)
                return null;

            return assemblyInfo.Types.FirstOrDefault(p => SummaryHelper.IsSameType(p.Type, type));
        }
    }
}
