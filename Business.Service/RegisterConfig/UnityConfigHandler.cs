
using Microsoft.Practices.Unity;
using Service.Core.Interfaces;
using Service.Contracts.Services;
using Business.Service.Order;
using Service.Contracts.Services.Order;
using Business.Service.Interfaces;

namespace Business.Service
{
    public class UnityConfigHandler : IUnityConfigHandler
    {
        public void ConfigureContainer(IUnityContainer container)
        {
            container
                   .RegisterType<IServiceCommon, ServiceCommon>()

                   .RegisterType<IService2, Service2>()
                   .RegisterType<IOrderBatchService, OrderBatchService>()
                   .RegisterType<IOrderService, OrderService>()
                   ;
        }
    }
}