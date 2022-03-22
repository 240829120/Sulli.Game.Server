using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Server
{
    /// <summary>
    /// 返回数据类型
    /// </summary>
    public class ResponseDataType
    {
        /// <summary>
        /// 返回数据类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="detail">描述</param>
        protected ResponseDataType(int type, string detail)
        {
            this.Type = type;
            this.Detail = detail;

            Pool.Add(type, this);
        }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// 返回数据类型池
        /// </summary>
        protected static Dictionary<int, ResponseDataType> Pool = new Dictionary<int, ResponseDataType>();

        /// <summary>
        /// 空
        /// </summary>
        public static readonly ResponseDataType Empty = new ResponseDataType(0, "普通请求返回");
    }
}
