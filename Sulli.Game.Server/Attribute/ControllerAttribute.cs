using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 控制器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ControllerAttribute : Attribute
    {
        /// <summary>
        /// 概要
        /// </summary>
        public string Summary { get; set; }
    }
}
