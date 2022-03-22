using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core.Verification
{
    /// <summary>
    /// 验证上下文
    /// </summary>
    public class ValidatorContext
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public List<string> Errors { get; private set; } = new List<string>();

        /// <summary>
        /// 返回错误字符串
        /// </summary>
        /// <returns></returns>
        public string GetErrorString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string item in this.Errors)
            {
                sb.AppendLine(item);
            }

            return sb.ToString();
        }
    }
}
