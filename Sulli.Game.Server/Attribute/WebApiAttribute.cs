using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 处理 Http / Https 请求
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class WebApiAttribute : ActionBaseAttribute
    {
        /// <summary>
        /// 方法类型
        /// </summary>
        public override ActionType Type => ActionType.WebApi;

        /// <summary>
        /// 请求方式
        /// </summary>
        public HttpMethod Method { get; set; } = HttpMethod.POST;

        /// <summary>
        /// POST请求
        /// </summary>
        public const string POST = "POST";

        /// <summary>
        /// GET请求
        /// </summary>
        public const string GET = "GET";

        /// <summary>
        /// 获取支持的方法集合
        /// </summary>
        /// <returns>支持的方法集合</returns>
        public override List<string> GetMethods()
        {
            List<string> list = new List<string>();

            if (this.Method.HasFlag(HttpMethod.POST))
            {
                list.Add(POST);
            }
            if (this.Method.HasFlag(HttpMethod.GET))
            {
                list.Add(GET);
            }

            return list;
        }
    }
}
