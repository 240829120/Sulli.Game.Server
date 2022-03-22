using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 文本返回
    /// </summary>
    public class StringResult : IResult
    {
        /// <summary>
        /// 文本返回
        /// </summary>
        /// <param name="value">值</param>
        public StringResult(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; private set; }
    }
}
