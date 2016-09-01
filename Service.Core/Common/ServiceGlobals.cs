using System;
using System.Collections.Generic;
using System.Linq;
//using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
//using Microsoft.AspNet.Identity;
using System.Configuration;
using Framework.Core.Pattern;
using System.ServiceModel.Web;

namespace Service.Core
{
    public partial class ServiceGlobals : ServiceLocator<IServiceGlobals, ServiceGlobals>, IServiceGlobals
    {
        protected override Func<IServiceGlobals> GetFactory()
        {
            return () => new ServiceGlobals();
        }       

        ////public bool IsProductMode
        ////{
        ////    get
        ////    {
        ////        string strIsProductMode = ConfigurationManager.AppSettings["IsProductMode"];
        ////        bool mIsProductMode = strIsProductMode == null ? false : bool.Parse(strIsProductMode);
        ////        return mIsProductMode;
        ////    }
        ////}
        //public int CurrentUserSchoolID
        //{
        //    get
        //    {
        //        int nValue = 1;
        //        var claimsUser = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
        //        if (claimsUser != null)
        //        {
        //            var claimSchool = claimsUser.FindFirst("SchoolID");
        //            if (claimSchool != null)
        //            {
        //                nValue = Int32.Parse(claimSchool.Value);
        //            }
        //        }
        //        return nValue;
        //    }
        //}
        //public string CurrentUserID
        //{
        //    get
        //    {
        //        string sValue = null;
        //        var oUser = Thread.CurrentPrincipal.Identity;
        //        if (oUser != null)
        //        {
        //            sValue = oUser.GetUserId();
        //        }
        //        return sValue;
        //    }
        //}

        //public string CurrentUserName
        //{
        //    get
        //    {
        //        string sValue = null;
        //        var oUser = Thread.CurrentPrincipal.Identity;
        //        if (oUser != null)
        //        {
        //            sValue = oUser.GetUserName();
        //        }
        //        return sValue;
        //    }
        //}

        //public string CurrentDisplayName
        //{
        //    get
        //    {
        //        string strValue = "";
        //        var claimsUser = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
        //        if (claimsUser != null)
        //        {
        //            var claim = claimsUser.FindFirst("DisplayName");
        //            if (claim != null)
        //            {
        //                strValue = claim.Value;
        //            }
        //        }
        //        return strValue;
        //    }
        //}
        public string CurrentUserSessionID
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
