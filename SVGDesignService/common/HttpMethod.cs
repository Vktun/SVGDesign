using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SVGDesignService.common
{
    public static class HttpMethods
    {
        public static async Task<string> HttpGetAsync(string url, Encoding encoding)
        {
            HttpClient httpClient = new HttpClient();
            var data = await httpClient.GetByteArrayAsync(url);
            var result = encoding.GetString(data);
            return result;
        }
        public static string HttpGet(string url, Encoding encoding)
        {
            HttpClient httpClient = new HttpClient();
            var temp = httpClient.GetByteArrayAsync(url);
            temp.Wait();
            var result = encoding.GetString(temp.Result);
            return result;
        }
        public static T HttpGet<T>(string url, Encoding encoding)
        {
            HttpClient httpClient = new HttpClient();
            var temp = httpClient.GetByteArrayAsync(url);
            temp.Wait();
            var result = encoding.GetString(temp.Result);
            T model = default(T);
            var jsonSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            model = JsonConvert.DeserializeObject<T>(result,jsonSetting);
            return model;
        }
        /// <summary>
        /// POST 异步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream"></param>
        /// <param name="encoding"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<string> HttpPostAsync(string url, Dictionary<string, string> formData = null, Encoding encoding = null, int timeOut = 10000)
        {
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            MemoryStream ms = new MemoryStream();
            formData.FillFormDataStream(ms);//填充formData
            HttpContent hc = new StreamContent(ms);
            hc.Headers.Add("Timeout", timeOut.ToString());
            hc.Headers.Add("KeepAlive", "true");

            var r = await client.PostAsync(url, hc);
            byte[] tmp = await r.Content.ReadAsByteArrayAsync();

            return encoding.GetString(tmp);
        }
        /// <summary>
        /// Post请求返回实体 
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求数据</param>
        /// <returns>实体</returns>
        public static T HttpPost<T>(string url, string postData)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip });
            HttpResponseMessage response = null;
            try
            {
                httpClient.MaxResponseContentBufferSize = 256000;
                httpClient.DefaultRequestHeaders.Add("user-agent", "host");
                httpClient.CancelPendingRequests();
                httpClient.DefaultRequestHeaders.Clear();
                HttpContent httpContent = new StringContent(postData);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                T result = default(T);
                Task<HttpResponseMessage> taskResponse = httpClient.PostAsync(url, httpContent);
                taskResponse.Wait();
                response = taskResponse.Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<Stream> taskStream = response.Content.ReadAsStreamAsync();
                    taskStream.Wait();
                    Stream dataStream = taskStream.Result;
                    StreamReader reader = new StreamReader(dataStream);
                    string s = reader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<T>(s);
                }
                return result;
            }
            catch
            {
                return default(T);
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }
                if (httpClient != null)
                {
                    httpClient.Dispose();

                }

            }

        }

        /// <summary>
        /// POST 同步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream"></param>
        /// <param name="encoding"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string HttpPost(string url, Dictionary<string, string> formData = null, Encoding encoding = null, int timeOut = 10000)
        {

            HttpClientHandler handler = new HttpClientHandler();

            HttpClient client = new HttpClient(handler);
            MemoryStream ms = new MemoryStream();
            formData.FillFormDataStream(ms);//填充formData
            HttpContent hc = new StreamContent(ms);
            hc.Headers.Add("Timeout", timeOut.ToString());
            hc.Headers.Add("KeepAlive", "true");

            var t = client.PostAsync(url, hc);
            t.Wait();
            var t2 = t.Result.Content.ReadAsByteArrayAsync();
            return encoding.GetString(t2.Result);
        }
        /// <summary>
        /// 组装QueryString的方法
        /// 参数之间用&连接，首位没有符号，如：a=1&b=2&c=3
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        public static string GetQueryString(this Dictionary<string, string> formData)
        {
            if (formData == null || formData.Count == 0)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();

            var i = 0;
            foreach (var kv in formData)
            {
                i++;
                sb.AppendFormat("{0}={1}", kv.Key, kv.Value);
                if (i < formData.Count)
                {
                    sb.Append("&");
                }
            }

            return sb.ToString();
        }
        /// <summary>
        /// 填充表单信息的Stream
        /// </summary>
        /// <param name="formData"></param>
        /// <param name="stream"></param>
        static void FillFormDataStream(this Dictionary<string, string> formData, Stream stream)
        {
            string dataString = GetQueryString(formData);
            var formDataBytes = formData == null ? new byte[0] : Encoding.UTF8.GetBytes(dataString);
            stream.Write(formDataBytes, 0, formDataBytes.Length);
            stream.Seek(0, SeekOrigin.Begin);//设置指针读取位置
        }
    }
}
