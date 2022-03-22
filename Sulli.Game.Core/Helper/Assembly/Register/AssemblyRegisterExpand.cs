using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 程序集注册扩展
    /// </summary>
    public static class AssemblyRegisterExpand
    {
        /// <summary>
        /// 所有类型
        /// </summary>
        /// <param name="info">程序集注册器</param>
        /// <returns>程序集注册器</returns>
        public static AssemblyRegisterInfo All(this AssemblyRegisterInfo info)
        {
            AssemblyRegisterRuleAll rule = new AssemblyRegisterRuleAll(info.Assembly);

            info.Rules.Add(rule);

            return info;
        }

        /// <summary>
        /// 包含类型
        /// </summary>
        /// <param name="info">程序集注册器</param>
        /// <param name="types">需要包含的类型</param>
        /// <returns>程序集注册器</returns>
        public static AssemblyRegisterInfo Include(this AssemblyRegisterInfo info, params Type[] types)
        {
            AssemblyRegisterRuleInclude rule = new AssemblyRegisterRuleInclude(info.Assembly);
            rule.Types = types.ToList();
            if (!rule.CheckTypes())
            {
                throw new Exception("type`s assembly is error.");
            }

            info.Rules.Add(rule);

            return info;
        }

        /// <summary>
        /// 包含类型
        /// </summary>
        /// <param name="info">程序集注册器</param>
        /// <param name="types">需要包含的类型</param>
        /// <returns>程序集注册器</returns>
        public static AssemblyRegisterInfo Include(this AssemblyRegisterInfo info, params string[] types)
        {
            AssemblyRegisterRuleInclude rule = new AssemblyRegisterRuleInclude(info.Assembly);
            foreach (string type in types)
            {
                rule.Types.Add(AssemblyHelper.GetType(info.Assembly, type));
            }
            if (!rule.CheckTypes())
            {
                throw new Exception("type`s assembly is error.");
            }

            info.Rules.Add(rule);

            return info;
        }

        /// <summary>
        /// 排除类型
        /// </summary>
        /// <param name="info">程序集注册器</param>
        /// <param name="types">需要包含的类型</param>
        /// <returns>程序集注册器</returns>
        public static AssemblyRegisterInfo Exclude(this AssemblyRegisterInfo info, params Type[] types)
        {
            AssemblyRegisterRuleExclude rule = new AssemblyRegisterRuleExclude(info.Assembly);
            rule.Types = types.ToList();
            if (!rule.CheckTypes())
            {
                throw new Exception("type`s assembly is error.");
            }

            info.Rules.Add(rule);

            return info;
        }

        /// <summary>
        /// 排除类型
        /// </summary>
        /// <param name="info">程序集注册器</param>
        /// <param name="types">需要包含的类型</param>
        /// <returns>程序集注册器</returns>
        public static AssemblyRegisterInfo Exclude(this AssemblyRegisterInfo info, params string[] types)
        {
            AssemblyRegisterRuleExclude rule = new AssemblyRegisterRuleExclude(info.Assembly);
            foreach (string type in types)
            {
                rule.Types.Add(AssemblyHelper.GetType(info.Assembly, type));
            }
            if (!rule.CheckTypes())
            {
                throw new Exception("type`s assembly is error.");
            }

            info.Rules.Add(rule);

            return info;
        }

        /// <summary>
        /// 正则表达式
        /// </summary>
        /// <param name="info">程序集注册器</param>
        /// <param name="regex">正则表达式</param>
        /// <returns>程序集注册器</returns>
        public static AssemblyRegisterInfo Regex(this AssemblyRegisterInfo info, Regex regex)
        {
            AssemblyRegisterRuleRegex rule = new AssemblyRegisterRuleRegex(info.Assembly);
            rule.Regex = regex;

            info.Rules.Add(rule);

            return info;
        }
    }
}
