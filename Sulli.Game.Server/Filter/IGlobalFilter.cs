using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 全局拦截器
    /// </summary>
    public interface IGlobalFilter : IFilter
    {
        /// <summary>
        /// 匹配正则表达式
        /// </summary>
        string Regex { get; set; }
    }
}
