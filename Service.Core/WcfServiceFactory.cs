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

            var behaviour = host.Description.Behaviors.Find<ServiceBehaviorAttribute>();
            //behaviour.InstanceContextMode = InstanceContextMode.PerCall;
            //behaviour.ConcurrencyMode = ConcurrencyMode.Multiple;

            Framework.Core.Logging.Log.Instance.LogInfo("CreateServiceHost");

            var basicBinding = new BasicHttpBinding
            {
                //OpenTimeout = new TimeSpan(0, 0, 20),
                //CloseTimeout = new TimeSpan(0, 0, 20),
                //ReceiveTimeout = new TimeSpan(0, 0, 20),
                //SendTimeout = new TimeSpan(0, 0, 20)
            };
            ServiceEndpoint endpoint_basic = host.AddServiceEndpoint(serviceType.GetInterfaces().First(), basicBinding, "");
            
            var webBinding = new WebHttpBinding
            {
                //OpenTimeout = new TimeSpan(0, 0, 20),
                //CloseTimeout = new TimeSpan(0, 0, 20),
                //ReceiveTimeout = new TimeSpan(0, 0, 20),
                //SendTimeout = new TimeSpan(0, 0, 20)
            };
            ServiceEndpoint endpoint = host.AddServiceEndpoint(serviceType.GetInterfaces().First(), webBinding, "restful");

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