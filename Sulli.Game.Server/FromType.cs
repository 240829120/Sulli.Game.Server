using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 源类型
    /// </summary>
    public enum FromType
    {
        /// <summary>
        /// 从Body中获取
        /// </summary>
        FromBody,
        /// <summary>
        /// 从Body项中获取
        /// </summary>
        FromBodyItem,
        /// <summary>
        /// 从头部数据获取
        /// </summary>
        FromHeader,
        /// <summary>
        /// 从查询参数中获取
        /// </summary>
        FromQuery
    }
}
