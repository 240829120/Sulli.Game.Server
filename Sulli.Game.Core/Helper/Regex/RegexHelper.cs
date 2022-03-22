using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 正则表达式辅助类
    /// </summary>
    public static class RegexHelper
    {
        /// <summary>
        /// Email验证
        /// </summary>
        public static readonly Regex Email = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
    }
}
