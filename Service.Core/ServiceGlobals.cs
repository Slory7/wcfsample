using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core
{
    public class ServiceGlobals
    {
        static IUnityContainer _container;
        public static IUnityContainer UnityContainer
        {
            get { return _container; }
            internal set { _container = value; }
        }
        public static string CurrentUserSessionID
        {
            get
            {
                if (WebOperationContext.Current != null)
                {
                    var headerValue = WebOperationContext.Current.IncomingRequest.Headers["UserSessionID"];
                    return headerValue;
                }
                return null;
            }
        }
    }
}
