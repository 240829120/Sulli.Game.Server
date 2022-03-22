using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 程序集注册规则 -- 排除类型
    /// </summary>
    public class AssemblyRegisterRuleExclude : IAssemblyRegisterRule
    {
        /// <summary>
        /// 程序集注册规则 -- 排除类型
        /// </summary>
        /// <param name="assembly">程序集</param>
        public AssemblyRegisterRuleExclude(Assembly assembly)
        {
            this.Assembly = assembly;
        }

        /// <summary>
        /// 程序集
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        ///  包含的类型集合
        /// </summary>
        public List<Type> Types { get; set; } = new List<Type>();

        /// <summary>
        /// 检测类型
        /// </summary>
        /// <returns>是否通过检测</returns>
        public bool CheckTypes()
        {
            return Types.All(p => SummaryHelper.IsSameAssembly(p.Assembly, this.Assembly));
        }

        /// <summary>
        /// 是否可以注册
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否可以注册</returns>
        public bool CanRegister(Type type)
        {
            return !this.Types.Any(p => SummaryHelper.IsSameType(p, type));
        }
    }
}
