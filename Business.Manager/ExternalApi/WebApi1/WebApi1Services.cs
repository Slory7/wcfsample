using Business.Manager.Common;
using Framework.Core.Net.Http;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager.ExternalApi.WebApi1
{
    public interface IWebApi1Services
    {
        Task<ResultData> GetVoucher();
    }
    public class WebApi1Services : IWebApi1Services
    {
        const string baseUrl = "http://localhost:50000";
        private IWebApi1Token _token;

        public WebApi1Services(
            IWebApi1Token token
            )
        {
            _token = token;
        }
        public async Task<ResultData> GetVoucher()
        {
            var retResult = new ResultData();
            var tokenResult = _token.GetToken();
            if (tokenResult.Status == ResultStatus.Success)
            {
                string strData = @"{
                      ""SchoolID"": 1,
                      ""StudentCode"": ""xx""
                    }
            ";
                var httpResult = await HttpWebClient.Instance.PostAsync(baseUrl, "/api/q", strData, ContentType.json, tokenResult.Result);
                retResult.Message = httpResult.Message ?? httpResult.Content;
                retResult.Status = httpResult.ToResultStatus();
            }
            else
            {
                retResult = tokenResult;
            }
            return retResult;
        }
    }
}
