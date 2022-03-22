using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 文件Buffer返回
    /// </summary>
    public class FileBufferResult : IResult
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件数据
        /// </summary>
        public byte[] Buffer { get; set; }
    }
}
