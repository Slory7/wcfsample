using Microsoft.Practices.Unity;
using ServiceContracts;
using Unity.Wcf;
using WcfService1.Business;

namespace WcfService1
{
    public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
            // register all your components with the container here
            container
               .RegisterType<IDataProcessor, DataProcessor>()
               .RegisterType<IService2, Service2>()
               .RegisterType<IService3, Service3>()
               ;
            //.RegisterType<DataContext>(new HierarchicalLifetimeManager());
        }
    }
}