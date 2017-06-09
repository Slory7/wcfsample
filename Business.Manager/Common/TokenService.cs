using Framework.Core.Net.Http;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Caching;
using System.Text;

namespace Business.Manager.Common
{
    public interface ITokenService
    {
        ResultData<TokenInfo> GetToken();
    }
    /// <summary>
    /// Token Service Helper
    /// </summary>
    /// <example>
    /// public interface IYHTokenService : ITokenService
    /// {
    /// }
    /// public class YHTokenService : TokenService, IYHTokenService
    /// {
    ///   public YHTokenService(IConfigService _ConfigService)
    ///     : base(
    ///              _ConfigService.GetConfigValue<string>(Constant.ConfigNames.YHServiceUrl)
    ///              , "/token"
    ///              , _ConfigService.GetConfigValue<string>(Constant.ConfigNames.YH__client_id)
    ///              , _ConfigService.GetConfigValue<string>(Constant.ConfigNames.YH__client_secret)
    ///              , _ConfigService.GetConfigValue<string>(Constant.ConfigNames.YH_username)
    ///              , _ConfigService.GetConfigValue<string>(Constant.ConfigNames.YH_password)
    ///            )
    ///  {
    ///   }
    /// }
    /// </example>
    public class TokenService : ITokenService
    {
        static object lockObj = new object();
        readonly string _baseUrl, _tokenRelativeUrl, _client_id, _client_secret, _username, _password;
        public TokenService(string baseUrl, string tokenRelativeUrl, string client_id, string client_secret, string username, string password)
        {
            _baseUrl = baseUrl;
            _tokenRelativeUrl = tokenRelativeUrl;
            _client_id = client_id;
            _client_secret = client_secret;
            _username = username;
            _password = password;
        }
        public ResultData<TokenInfo> GetToken()
        {
            string cacheKey = ("Token_" + _baseUrl + "_" + _tokenRelativeUrl + "_" + _client_id + "_" + _username).GetHashCode().ToString();
            var token = MemoryCache.Default.Get(cacheKey) as TokenInfo;
            var retResult = new ResultData<TokenInfo>()
            {
                Result = token
            };
            if (token == null)
            {
                lock (lockObj)
                {
                    token = MemoryCache.Default.Get(cacheKey) as TokenInfo;
                    if (token == null)
                    {
                        var dic = new Dictionary<string, string>();
                        dic.Add("grant_type", "password");
                        dic.Add("client_id", _client_id);
                        dic.Add("client_secret", _client_secret);
                        dic.Add("username", _username);
                        dic.Add("password", _password);
                        var httpResult = HttpWebClient.Instance.PostAsync(_baseUrl, _tokenRelativeUrl, dic, isLogData: false).Result;
                        if (httpResult.IsSuccessStatusCode)
                        {
                            TokenResult objTokenResult = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResult>(httpResult.Content);

                            if (objTokenResult != null && objTokenResult.access_token != null)
                            {
                                int expireTime = objTokenResult.expires_in - 5 * 60;
                                token = new TokenInfo()
                                {
                                    Access_Token = objTokenResult.access_token,
                                    Token_Type = objTokenResult.token_type
                                };
                                retResult.Result = token;
                                MemoryCache.Default.Set(cacheKey, token, DateTime.Now.AddSeconds(expireTime));
                            }
                        }
                        else
                        {
                            retResult.Status = httpResult.ToResultStatus();
                            retResult.Message = httpResult.Message;
                        }
                    }
                }
            }
            return retResult; ;
        }

        #region Internal Class

        class TokenResult
        {
            public string access_token { get; set; }

            public string token_type { get; set; }

            public int expires_in { get; set; }
        }

        #endregion
    }
}
