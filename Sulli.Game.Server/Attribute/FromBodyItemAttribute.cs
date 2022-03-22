using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 参数来源于Body的子项
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class FromBodyItemAttribute : FromBaseAttribute
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override FromType Type => FromType.FromBodyItem;

        /// <summary>
        /// 名称
        /// </summary>
        public override string Name { get; set; }
    }
}
