using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sulli.Game.Core
{
    /// <summary>
    /// Http辅助类
    /// </summary>
    public static class HttpHelper
    {
        /// <summary>
        /// HTTP请求
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="uri">地址</param>
        /// <param name="data">数据</param>
        /// <param name="header">数据头</param>
        /// <returns>返回结果</returns>
        public static async Task<T?> PostAsync<T>(Uri uri, object? data = null, Dictionary<string, string>? header = null)
        {
            string json_response = await PostAsync(uri, data, header);

            T? result = JsonConvert.DeserializeObject<T>(json_response);

            return result;
        }

        /// <summary>
        /// HTTP请求
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="uri">地址</param>
        /// <param name="data">数据</param>
        /// <param name="header">数据头</param>
        /// <returns>返回结果</returns>
        public static async Task<string> PostAsync(Uri uri, object? data = null, Dictionary<string, string>? header = null)
        {
            string json_request = data == null ? string.Empty : JsonConvert.SerializeObject(data);
            byte[] buffer_request = Encoding.UTF8.GetBytes(json_request);

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Proxy = null;
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            request.ContentLength = buffer_request.Length;

            if (header != null && header.Count > 0)
            {
                foreach (var kv in header)
                {
                    request.Headers.Add(kv.Key, kv.Value);
                }
            }

            using (Stream stream_request = request.GetRequestStream())
            {
                await stream_request.WriteAsync(buffer_request, 0, buffer_request.Length);
            }

            using (WebResponse response = request.GetResponse())
            using (Stream stream_response = response.GetResponseStream())
            using (StreamReader stream_reader = new StreamReader(stream_response, Encoding.UTF8))
            {
                string json_response = await stream_reader.ReadToEndAsync();

                return json_response;
            }
        }
    }
}
