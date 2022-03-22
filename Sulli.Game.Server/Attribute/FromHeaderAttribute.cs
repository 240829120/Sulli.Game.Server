using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 参数来源于头部数据
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class FromHeaderAttribute : FromBaseAttribute
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override FromType Type => FromType.FromHeader;

        /// <summary>
        /// 名称
        /// </summary>
        public override string Name { get; set; }
    }
}
