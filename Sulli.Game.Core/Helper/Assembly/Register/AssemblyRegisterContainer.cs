using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 程序集注册容器
    /// </summary>
    public class AssemblyRegisterContainer
    {
        /// <summary>
        /// 创建注册信息
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns>注册信息</returns>
        public AssemblyRegisterInfo Create(Assembly assembly)
        {
            AssemblyRegisterInfo info = new AssemblyRegisterInfo(assembly);
            this.Infos.Add(info);

            return info;
        }

        /// <summary>
        /// 创建注册信息
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns>注册信息</returns>
        public AssemblyRegisterInfo Create(string assembly)
        {
            AssemblyRegisterInfo info = new AssemblyRegisterInfo(assembly);
            this.Infos.Add(info);

            return info;
        }

        /// <summary>
        /// 注册信息
        /// </summary>
        public List<AssemblyRegisterInfo> Infos { get; private set; } = new List<AssemblyRegisterInfo>();

        /// <summary>
        /// 是否包含程序集
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns>是否包含程序集</returns>
        public bool Has(Assembly assembly)
        {
            return this.Infos.Any(p => SummaryHelper.IsSameAssembly(p.Assembly, assembly));
        }

        /// <summary>
        /// 获取程序集注册信息
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns>程序集注册信息</returns>
        public AssemblyRegisterInfo Get(Assembly assembly)
        {
            return this.Infos.FirstOrDefault(p => SummaryHelper.IsSameAssembly(p.Assembly, assembly));
        }
    }
}
