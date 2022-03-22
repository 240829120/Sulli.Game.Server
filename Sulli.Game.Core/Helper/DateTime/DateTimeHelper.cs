using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 日期辅助类
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// 是否是测试模式
        /// </summary>
        public static bool IsTestMode { get; set; }

        /// <summary>
        /// 参照日期
        /// </summary>
        public static DateTime ReferenceDate { get; set; } = DateTime.Now.Date;

        /// <summary>
        /// 便宜量
        /// </summary>
        public static TimeSpan OffsetTime { get; set; } = TimeSpan.MinValue;

        /// <summary>
        /// 当前时间
        /// </summary>
        public static DateTime Now
        {
            get
            {
#if DEBUG
                return IsTestMode ? ReferenceDate.Date + DateTime.Now.TimeOfDay + OffsetTime : DateTime.Now;
#else
                return DateTime.Now;
#endif
            }
        }
    }
}
