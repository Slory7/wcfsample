using Framework.Core.Net.Http;
using Service.Contracts;
using System.Net;

namespace Business.Manager.Common
{
    public static class HttpResultExtension
    {
        public static ResultStatus ToResultStatus(this HttpResult result)
        {
            if (result.IsSuccessStatusCode)
                return ResultStatus.Success;
            switch (result.Status)
            {
                case HttpStatusCode.NotFound:
                    return ResultStatus.NotFound;
                case HttpStatusCode.Conflict:
                    return ResultStatus.Conflict;
                case HttpStatusCode.Forbidden:
                    return ResultStatus.Forbidden;
                case (HttpStatusCode)422:
                    return ResultStatus.BadLogic;
                case HttpStatusCode.BadRequest:
                    return ResultStatus.BadData;
                case HttpStatusCode.Unauthorized:
                    return ResultStatus.Unauthorized;
                case HttpStatusCode.InternalServerError:
                default:
                    return ResultStatus.Error;
            }
        }
    }
}
