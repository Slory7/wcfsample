using System.Net;

namespace Framework.Core.Net.Http
{
    public class HttpResult
    {
        public HttpStatusCode Status { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public string Content { get; set; }
        public string Message { get; set; }        
    }
}
