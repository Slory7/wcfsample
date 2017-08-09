using Microsoft.Practices.Unity;
using System.Reflection;
using Unity.Wcf;
using System.Linq;
using Repository.Pattern.Interface;
using Repository.Pattern;
using System;
using Service.Core.Interfaces;
using Framework.Core.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;

namespace Service.Core
{
    public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
            UnityConfig.ConfigureContainer(container);
        }
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = base.CreateServiceHost(serviceType, baseAddresses);
            ServiceEndpoint endpoint_basic = host.AddServiceEndpoint(serviceType.GetInterfaces().First(), new BasicHttpBinding(), "");

            ServiceEndpoint endpoint = host.AddServiceEndpoint(serviceType.GetInterfaces().First(), new WebHttpBinding(), "restful");

            WebHttpBehavior behavior = new WebHttpBehavior()
            {
                DefaultBodyStyle = WebMessageBodyStyle.Wrapped,
                DefaultOutgoingRequestFormat = WebMessageFormat.Json,
                DefaultOutgoingResponseFormat = WebMessageFormat.Json
            };
            endpoint.Behaviors.Add(behavior);

            return host;
        }
    }
}