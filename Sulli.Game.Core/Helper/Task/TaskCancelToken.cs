using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 任务取消标志
    /// </summary>
    public class TaskCancelToken<T>
    {
        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCancel { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }
}
