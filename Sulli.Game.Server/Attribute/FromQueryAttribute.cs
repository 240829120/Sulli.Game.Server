using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 参数来源于请求url
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class FromQueryAttribute : FromBaseAttribute
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override FromType Type => FromType.FromQuery;

        /// <summary>
        /// 名称
        /// </summary>
        public override string Name { get; set; }
    }
}
