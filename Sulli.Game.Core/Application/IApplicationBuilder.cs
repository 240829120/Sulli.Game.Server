using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public interface IApplicationBuilder
    {
        /// <summary>
        /// 构建
        /// </summary>
        Task BuildAsync();
    }
}
