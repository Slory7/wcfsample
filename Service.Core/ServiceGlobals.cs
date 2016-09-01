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
    public partial class ServiceGlobals
    {
        static IUnityContainer _container;
        public static IUnityContainer UnityContainer
        {
            get { return _container; }
            internal set { _container = value; }
        }       
    }
}
