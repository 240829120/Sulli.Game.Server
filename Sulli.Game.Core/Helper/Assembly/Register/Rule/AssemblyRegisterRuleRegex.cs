using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 程序集注册规则 -- 正则表达式
    /// </summary>
    public class AssemblyRegisterRuleRegex : IAssemblyRegisterRule
    {
        /// <summary>
        /// 程序集注册规则 -- 正则表达式
        /// </summary>
        /// <param name="assembly">程序集</param>
        public AssemblyRegisterRuleRegex(Assembly assembly)
        {
            this.Assembly = assembly;
        }

        /// <summary>
        /// 程序集
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public Regex Regex { get; set; }

        /// <summary>
        /// 是否可以注册
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否可以注册</returns>
        public bool CanRegister(Type type)
        {
            return this.Regex.IsMatch(type.FullName);
        }
    }
}
