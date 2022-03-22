using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 程序集注册规则 -- 所有类型
    /// </summary>
    public class AssemblyRegisterRuleAll : IAssemblyRegisterRule
    {
        /// <summary>
        /// 程序集注册规则 -- 所有类型
        /// </summary>
        /// <param name="assembly">程序集</param>
        public AssemblyRegisterRuleAll(Assembly assembly)
        {
            this.Assembly = assembly;
        }

        /// <summary>
        /// 程序集
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// 是否可以注册
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否可以注册</returns>
        public bool CanRegister(Type type)
        {
            return SummaryHelper.IsSameAssembly(this.Assembly, type.Assembly);
        }
    }
}
