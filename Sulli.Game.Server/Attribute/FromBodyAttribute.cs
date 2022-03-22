using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 参数从内容提取
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class FromBodyAttribute : FromBaseAttribute
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override FromType Type => FromType.FromBody;

        /// <summary>
        /// 名称
        /// </summary>
        public override string Name { get; set; }
    }
}
