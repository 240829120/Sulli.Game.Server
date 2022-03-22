using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 处理 WebSocket 请求
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class WebSocketAttribute : ActionBaseAttribute
    {
        /// <summary>
        /// 方法类型
        /// </summary>
        public override ActionType Type => ActionType.WebSocket;

        /// <summary>
        /// 获取支持的方法集合
        /// </summary>
        /// <returns>支持的方法集合</returns>
        public override List<string> GetMethods()
        {
            List<string> list = new List<string>();

            return list;
        }
    }
}
