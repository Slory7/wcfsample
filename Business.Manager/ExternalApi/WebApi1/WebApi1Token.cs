using Business.Manager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager.ExternalApi.WebApi1
{
    public interface IWebApi1Token : ITokenService { }

    public class WebApi1Token : TokenService, IWebApi1Token
    {
        public WebApi1Token()
            : base(
                  "http://localhost:50000"
                  , "/token"
                  , "xx"
                  , "xx"
                  , "xx"
                  , "xx"
                )
        {
        }
    }
}
