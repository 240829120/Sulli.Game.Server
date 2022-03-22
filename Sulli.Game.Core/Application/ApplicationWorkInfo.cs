using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 应用程序工作信息
    /// </summary>
    public class ApplicationWorkInfo
    {
        /// <summary>
        /// 应用程序工作信息
        /// </summary>
        /// <param name="wait">工作等待</param>
        /// <param name="action">行为</param>
        public ApplicationWorkInfo(ApplicationWorkMode mode, TimeSpan wait, Action action)
        {
            this.BeginTime = DateTimeHelper.Now;
            this.Mode = mode;
            this.Wait = wait;
            this.Action = action;
        }

        /// <summary>
        /// 模式
        /// </summary>
        public ApplicationWorkMode Mode { get; private set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; private set; }

        /// <summary>
        /// 等待
        /// </summary>
        public TimeSpan Wait { get; private set; }

        /// <summary>
        /// 行为
        /// </summary>
        public Action Action { get; private set; }
    }
}
