using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 程序集注册信息
    /// </summary>
    public class AssemblyRegisterInfo
    {
        /// <summary>
        /// 程序集注册信息
        /// </summary>
        /// <param name="assembly">程序集</param>
        public AssemblyRegisterInfo(Assembly assembly)
        {
            this.Assembly = Assembly;
        }

        /// <summary>
        /// 程序集注册器
        /// </summary>
        /// <param name="assembly">程序集</param>
        public AssemblyRegisterInfo(string assembly)
        {
            this.Assembly = AssemblyHelper.GetAssembly(assembly);
        }

        /// <summary>
        /// 程序集
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// 注册项
        /// </summary>
        public List<IAssemblyRegisterRule> Rules { get; private set; } = new List<IAssemblyRegisterRule>();

        /// <summary>
        /// 是否可以注册
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否可以注册</returns>
        public bool CanRegister(Type type)
        {
            foreach (IAssemblyRegisterRule rule in this.Rules)
            {
                if (!rule.CanRegister(type))
                    return false;
            }

            return true;
        }
    }
}
