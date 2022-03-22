using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 应用程序信息
    /// </summary>
    public class ApplicationInfo
    {
        /// <summary>
        /// 本机IP
        /// </summary>
        public string LocalIP { get; set; }

        /// <summary>
        /// 应用程序信息
        /// </summary>
        public ApplicationInfo()
        {
            this.LocalIP = NetHelper.GetLocalIP();
        }
    }
}
