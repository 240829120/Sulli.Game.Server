using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// Json返回结果
    /// </summary>
    public class JsonResult : IResult
    {
        /// <summary>
        /// Json返回结果
        /// </summary>
        /// <param name="value"></param>
        public JsonResult(object value)
        {
            this.Value = value;
        }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; private set; }
    }
}
