using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sulli.Game.Core
{
    /// <summary>
    /// 加密辅助类
    /// </summary>
    public static class EncryptionHelper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="context">加密内容</param>
        /// <returns>加密字符串</returns>
        //public static string MD5(string context)
        //{
        //    byte[] text = Encoding.UTF8.GetBytes(context);
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] output = md5.ComputeHash(text);
        //    string result = BitConverter.ToString(output).Replace("-", "");

        //    return result;
        //}
    }
}
