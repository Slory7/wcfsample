using Framework.Core.Reflection;
using Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using System.Web;
using System.Web.Routing;

namespace WcfService1
{
    public class HostServices
    {
        public void RegisterServices()
        {
            TypeLocator objTypeLocator = new TypeLocator();
            var types = objTypeLocator.GetAllMatchingTypes((t) =>
            {
                return t.GetCustomAttribute(typeof(HostServiceAttribute), false) != null;
            });
            WcfServiceFactory factory = new WcfServiceFactory();
            foreach (var serviceType in types)
            {
                RouteTable.Routes.Add(
                    new ServiceRoute(serviceType.Name + ".svc", factory, serviceType));
            }
        }
    }
}
