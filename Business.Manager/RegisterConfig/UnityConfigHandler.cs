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
using Business.Manager.ExternalApi.WebApi1;

namespace Business.Manager.RegisterConfig
{
    public class UnityConfigHandler : IUnityConfigHandler
    {
        public void ConfigureContainer(IUnityContainer container)
        {
            container
                   .RegisterType<IManagerCommon, ManagerCommon>()

                   .RegisterType<IManager<BS_Order_SalesOrder_Batch>, OrderBatchManager>()
                   .RegisterType<IOrderBatchManager, OrderBatchManager>()

                   .RegisterType<IOrderBizManager, OrderBizManager>()

                   .RegisterType<IWebApi1Token, WebApi1Token>()
                   .RegisterType<IWebApi1Services, WebApi1Services>()
                   ;
        }
    }
}