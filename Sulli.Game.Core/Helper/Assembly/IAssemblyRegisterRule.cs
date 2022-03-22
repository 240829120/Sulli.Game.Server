using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 程序集注册规则
    /// </summary>
    public interface IAssemblyRegisterRule
    {
        /// <summary>
        /// 是否可以注册
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否可以注册</returns>
        bool CanRegister(Type type);
    }
}
