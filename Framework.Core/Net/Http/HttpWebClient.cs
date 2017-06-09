using Framework.Core.Pattern;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Net.Http
{
    public interface IHttpWebClient
    {
        Task<HttpResult> DeleteAsync(string baseUri, string relativeUri, TokenInfo token = null, int timeoutSecond = 0, bool isLogData = true);
        Task<HttpResult> GetAsync(string baseUri, string relativeUri, TokenInfo token = null, int timeoutSecond = 0, bool isLogData = true);
        Task<HttpResult> GetAsync(string baseUri, string relativeUri, IDictionary<string, string> parameters, TokenInfo token = null, int timeoutSecond = 0, bool isLogData = true);
        Task<HttpResult> PostAsync(string baseUri, string relativeUri, IDictionary<string, string> parameters, TokenInfo token = null, int timeoutSecond = 0, bool isLogData = true);
        Task<HttpResult> PostAsync(string baseUri, string relativeUri, string postData, ContentType contentType, TokenInfo token = null, int timeoutSecond = 0, bool isLogData = true);
        Task<HttpResult> PutAsync(string baseUri, string relativeUri, IDictionary<string, string> parameters, TokenInfo token = null, int timeoutSecond = 0, bool isLogData = true);
        Task<HttpResult> PutAsync(string baseUri, string relativeUri, string postData, ContentType contentType, TokenInfo token = null, int timeoutSecond = 0, bool isLogData = true);
        Task<HttpResult> SendAsync(string baseUri, string relativeUri, string postData, ContentType contentType, HttpMethod method, TokenInfo token = null, int timeoutSecond = 0, bool isLogData = true);
    }
    public class HttpWebClient : ServiceLocator<IHttpWebClient, HttpWebClient>, IHttpWebClient
    {
        protected override Func<IHttpWebClient> GetFactory()
        {
            return () => new HttpWebClient();
        }

        private HttpClient GetClient(string baseUri, TokenInfo token, int timeoutSecond)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 1000;

            HttpClient client = new HttpClient(
              new HttpClientHandler
              {
                  AutomaticDecompression = DecompressionMethods.GZip
                                           | DecompressionMethods.Deflate,
              });

            client.BaseAddress = new Uri(baseUri);

            client.DefaultRequestHeaders.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("zh-CN"));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("WCFApp", "1.0"));

            if (timeoutSecond > 0)
                client.Timeout = new TimeSpan(timeoutSecond * TimeSpan.TicksPerSecond);
            if (token != null)
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token.Token_Type, token.Access_Token);

            return client;
        }
        public async Task<HttpResult> GetAsync(string baseUri, string relativeUri, TokenInfo token = null, int timeoutSecond = 0, bool isLogData = true)
        {
            return await SendAsync(baseUri, relativeUri, null, ContentType.json, HttpMethod.Get, token, timeoutSecond, isLogData);
        }
        public async Task<HttpResult> GetAsync(string baseUri, string relativeUri, IDictionary<string, string> parameters, TokenInfo token = null, int timeoutSecond = 0, bool isLogData = true)
        {
            string rUrl = relativeUri;
            string urlParams = HttpHelper.BuildRequestParameters(parameters);
            if (!String.IsNullOrEmpty(urlParams))
            {
                rUrl += rUrl.Contains("?") ? "&" : "?";
                rUrl += urlParams;
            }
            return await SendAsync(baseUri, rUrl, null, ContentType.json, HttpMethod.Get, token, timeoutSecond, isLogData);
        }
        public async Task<HttpResult> PostAsync(string baseUri, string relativeUri, IDictionary<string, string> parameters, TokenInfo token = null, int timeoutSecond = 0, bool isLogData = true)
        {
            string postData = HttpHelper.BuildRequestParameters(parameters);
            return await PostAsync(baseUri, relativeUri, postData, ContentType.form, token, timeoutSecond, isLogData);
        }
        public async Task<HttpResult> PostAsync(string baseUri, string relativeUri, string postData, ContentType contentType, TokenInfo token = null, int timeoutSecond = 0, bool isLogData = true)
        {
            return await SendAsync(baseUri, relativeUri, postData, contentType, HttpMethod.Post, token, timeoutSecond, isLogData);
        }
        public async Task<HttpResult> PutAsync(string baseUri, string relativeUri, IDictionary<string, string> parameters, TokenInfo token = null, int timeoutSecond = 0, bool isLogData = true)
        {
            string postData = HttpHelper.BuildRequestParameters(parameters);
            return await PutAsync(baseUri, relativeUri, postData, ContentType.form, token, timeoutSecond, isLogData);
        }
        public async Task<HttpResult> PutAsync(string baseUri, string relativeUri, string postData, ContentType contentType, TokenInfo token = null, int timeoutSecond = 0, bool isLogData = true)
        {
            return await SendAsync(baseUri, relativeUri, postData, contentType, HttpMethod.Put, token, timeoutSecond, isLogData);
        }
        public async Task<HttpResult> DeleteAsync(string baseUri, string relativeUri, TokenInfo token = null, int timeoutSecond = 0, bool isLogData = true)
        {
            return await SendAsync(baseUri, relativeUri, null, ContentType.json, HttpMethod.Delete, token, timeoutSecond, isLogData);
        }
        public async Task<HttpResult> SendAsync(string baseUri, string relativeUri, string postData, ContentType contentType, HttpMethod method, TokenInfo token = null, int timeoutSecond = 0, bool isLogData = true)
        {
            HttpResult result = new HttpResult();
            StringBuilder sbLog = new StringBuilder();
            try
            {
                sbLog.AppendLine("HttpClient Log");
                sbLog.AppendFormat("Url:{0}\r\n", baseUri + relativeUri);
                sbLog.AppendFormat("Method:{0}\r\n", method.ToString());
                sbLog.AppendFormat("ContentType:{0}\r\n", contentType.ToString());
                if (!String.IsNullOrEmpty(postData))
                    sbLog.AppendFormat("Data:{0}\r\n", isLogData ? postData : "[SECURITY]");
                if (token != null)
                    sbLog.AppendFormat("Token:{0}\r\n", token.ToString().Substring(0, 10) + "...");

                using (var client = this.GetClient(baseUri, token, timeoutSecond))
                {
                    HttpContent httpContent = null;
                    if (postData != null)
                    {
                        httpContent = new StringContent(postData, Encoding.UTF8);
                        httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(this.GetContentType(contentType)) { CharSet = "utf-8" };
                    }
                    HttpRequestMessage requestMessage = new HttpRequestMessage(method, relativeUri) { Content = httpContent };
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    var response = await client.SendAsync(requestMessage);
                    watch.Stop();
                    sbLog.AppendFormat("ElapseTime:{0}\r\n", watch.ElapsedMilliseconds);

                    result.Status = response.StatusCode;
                    string strContent = await response.Content.ReadAsStringAsync();
                    result.Content = strContent;
                    result.IsSuccessStatusCode = response.IsSuccessStatusCode;
                    result.Message = response.ReasonPhrase;
                }
                if (!result.IsSuccessStatusCode)
                {
                    if (!String.IsNullOrEmpty(result.Content) && result.Content.StartsWith("{"))
                    {
                        try
                        {
                            using (var stream = new MemoryStream(Encoding.Default.GetBytes(result.Content)))
                            {
                                var serializer = new DataContractJsonSerializer(typeof(ResponseMsg));
                                var msgObj = serializer.ReadObject(stream) as ResponseMsg;
                                var msg = msgObj.Message ?? msgObj.msg ?? msgObj.error;
                                if (msg != null)
                                    result.Message = msg;
                            }
                        }
                        catch { }
                    }
                }
            }
            catch (Exception e)
            {
                result.Status = HttpStatusCode.InternalServerError;
                var ex = e.InnerException ?? e;
                result.Message = ex.Message;
            }
            sbLog.AppendFormat("Status:{0}-{1}\r\n", (int)result.Status, result.Status);
            if (result.Message != null)
                sbLog.AppendFormat("Message:{0}\r\n", result.Message);
            if (result.Content != null)
                sbLog.AppendFormat("Result:{0}", isLogData ? result.Content : "[SECURITY]");

            Logging.Log.Instance.LogInfo(sbLog.ToString());
            if (!result.IsSuccessStatusCode)
                Logging.Log.Instance.LogError(sbLog.ToString(), new Exception("HttpClient Error"));

            return result;
        }

        #region Private Methods

        private string GetContentType(ContentType cType)
        {
            switch (cType)
            {
                case ContentType.json: return "application/json";
                case ContentType.form: return "application/x-www-form-urlencoded";
                case ContentType.xml: return "text/xml";
                case ContentType.text: return "text/plain";
                default: return "";
            }
        }

        #endregion    

        #region Internal Class

        [DataContract]
        class ResponseMsg
        {
            [DataMember]
            public string Message { get; set; }

            [DataMember]
            public string msg { get; set; }

            [DataMember]
            public string error { get; set; }
        }

        #endregion
    }

    public enum ContentType
    {
        json,
        form,
        xml,
        text
    }
}
