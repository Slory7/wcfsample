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
            var hostTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetCustomAttribute(typeof(HostServiceAttribute), false) != null);
            WcfServiceFactory factory = new WcfServiceFactory();
            foreach(var serviceType in hostTypes)
            {
                RouteTable.Routes.Add(
                    new ServiceRoute(serviceType.Name + ".svc", factory, serviceType));
            }
        }
     }
}