using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace ERP.Core.Framework.Common.Helpers
{
    /// <summary>
    /// Http操作类
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// http/https请求响应
        /// </summary> 
        /// <param name="url">地址（要带上http或https）</param>
        /// <param name="parameters">提交数据</param>  
        /// <returns></returns>
        public static string Post(string url,Dictionary<string, string> parameters)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json"; 

            var buffer = FormatPostParameters(parameters, encoding); 
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException(), encoding))
            {
                return reader.ReadToEnd();
            }

        }

        /// <summary>
        /// http/https请求响应
        /// </summary> 
        /// <param name="url">地址（要带上http或https）</param> 
        /// <returns></returns>
        public static string Get(string url)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException(), encoding))
            {
                return reader.ReadToEnd();
            }

        }
       
        /// <summary>
        /// 格式化Post请求参数
        /// </summary>
        /// <param name="parameters">编码格式</param>
        /// <param name="dataEncoding">编码格式</param> 
        /// <returns></returns>
        private static byte[] FormatPostParameters(Dictionary<string, string> parameters, Encoding dataEncoding)
        {
            var contentType = "application/json";
            string sendContext = "";
            int i = 0;
            if (!string.IsNullOrEmpty(contentType) && contentType.ToLower().Trim() == "application/json")
            {
                sendContext = "{";
            }

            foreach (var para in parameters)
            {
                if (!string.IsNullOrEmpty(contentType) && contentType.ToLower().Trim() == "application/json")
                {
                    if (i > 0)
                    {
                        if (para.Value.StartsWith("{"))
                        {
                            sendContext += $@",""{para.Key}"":{para.Value}";
                        }
                        else
                        {
                            sendContext += $@",""{para.Key}"":""{para.Value}""";
                        }

                    }
                    else
                    {
                        if (para.Value.StartsWith("{"))
                        {
                            sendContext += $@"""{para.Key}"":{para.Value}";
                        }
                        else
                        {
                            sendContext += $@"""{para.Key}"":""{para.Value}""";
                        }

                    }
                }
                else
                {
                    if (i > 0)
                    {
                        sendContext += $"&{para.Key}={HttpUtility.UrlEncode(para.Value, dataEncoding)}";
                    }
                    else
                    {
                        sendContext = $"{para.Key}={HttpUtility.UrlEncode(para.Value, dataEncoding)}";
                    }
                }

                i++;
            }

            if (!string.IsNullOrEmpty(contentType) && contentType.ToLower().Trim() == "application/json")
            {
                sendContext += "}";
            }

            byte[] data = dataEncoding.GetBytes(sendContext);
            return data;
        }

         
    }
}
