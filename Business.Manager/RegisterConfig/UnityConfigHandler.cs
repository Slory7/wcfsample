using Business.Core;
using Microsoft.Practices.Unity;
using Repository.Pattern;
using Repository.Pattern.Interface;
using Service.Core.Interfaces;
using Data.Entities.Models;
using Business.Manager.Interfaces;
using Business.Manager.Order;
using Business.Manager.Order.Interfaces;
using Business.Core.Interfaces;
using Business.Core.BaseManager;
using Service.Contracts.ViewModels.Order;

namespace Business.Manager
{
    public class UnityConfigHandler : IUnityConfigHandler
    {
        public void ConfigureContainer(IUnityContainer container)
        {
            container
                   .RegisterType<IManagerCommon, ManagerCommon>()

                   .RegisterType<IOrderBatchManager, OrderBatchManager>()

                   .RegisterType<IOrderBizManager, OrderBizManager>()
                   ;
        }
    }
}