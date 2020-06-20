using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class HttpClientHelp
    {
        public static HttpClient _client = new HttpClient(new HttpClientHandler() { UseCookies = false });

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<Stream> Download(string url, HttpMethod httpMethod)
        {
            try
            {
                var httpRequestMessage = new HttpRequestMessage(httpMethod, new Uri(url))
                {
                    Version = HttpVersion.Version10
                };
                HttpResponseMessage response = await _client.SendAsync(httpRequestMessage);

                response.EnsureSuccessStatusCode();//用来抛异常的
                Stream responseBody = await response.Content.ReadAsStreamAsync();
                return responseBody;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="fileStream"></param>
        /// <param name="fileName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<T> Upload<T>(string url, FileStream fileStream, string fileName, string name = "file", Dictionary<string, string> pairs = null)where T:class,new()
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            if (pairs != null)
            {
                foreach (string key in pairs.Keys)
                {
                    content.Headers.Add(key, pairs[key]);
                }
            }
            string result;
            using (fileStream)
            {
                content.Add(new StreamContent(fileStream), name, fileName);
                result = await Post(url, content);
            }
            return string.IsNullOrEmpty(result) ? null : JsonConvert.DeserializeObject<T>(result);
        }


        /// <summary>
        /// Post 返回实体M
        /// </summary>
        public static async Task<M> Post<T,M>(string url, T obj, EMUN_HTTPCONTENT content_typ = EMUN_HTTPCONTENT.STRINGCONTENT, string mediaType = "application/json") where T : class, new() where M:class,new()
        {
            HttpContent content;
            if (content_typ == EMUN_HTTPCONTENT.STRINGCONTENT)
            {
                content = new StringContent(JsonConvert.SerializeObject(obj));  //当请求接口对象为对象时
            }
            else if (content_typ == EMUN_HTTPCONTENT.FORMURLENCODEDCONTENT)
            {
                content = new FormUrlEncodedContent(obj.ToDictionary());  //当请求接口参数为变量时
            }
            else
            {
                content = null;
            }
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType);
            string result = await Post(url, content);
            return string.IsNullOrEmpty(result) ? null : JsonConvert.DeserializeObject<M>(result);
        }

        /// <summary>
        /// Get 获取T
        /// </summary>
        /// <returns></returns>
        public static async Task<T> Get<T>(string url, T obj) where T : class, new()
        {
            var msg = await _client.GetAsync(url + "?" + obj.ObjToParam());
            string result = await GetStringResult(msg);
            return string.IsNullOrEmpty(result) ? null : JsonConvert.DeserializeObject<T>(result);
        }


        private static async Task<string> Post(string url, HttpContent httpContent)
        {
            var msg = await _client.PostAsync(url, httpContent);
            return await GetStringResult(msg);
        }

        private static async Task<string> GetStringResult(HttpResponseMessage msg)
        {
            string reslut = string.Empty;
            if (msg != null && msg.StatusCode == HttpStatusCode.OK)
            {
                using (msg)
                {
                    reslut = await msg.Content.ReadAsStringAsync();
                }
            }

            return reslut;
        }
    }

    public enum EMUN_HTTPCONTENT
    {
        STRINGCONTENT = 1,
        FORMURLENCODEDCONTENT = 2
    }
}
