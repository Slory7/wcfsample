using Data.Entities.Models;
using Microsoft.Practices.Unity;
using Repository.Pattern;
using Repository.Pattern.Interface;
using Service.Core.Interfaces;

namespace Data.Entities
{
    public class UnityConfigHandler : IUnityConfigHandler
    {
        public void ConfigureContainer(IUnityContainer container)
        {
            container
                   .RegisterType<IReadOnlyRepository<BS_Order_SalesOrder>, ReadOnlyRepository<BS_Order_SalesOrder>>()
                   .RegisterType<IReadOnlyRepository<BS_Order_SalesOrder_Batch>, ReadOnlyRepository<BS_Order_SalesOrder_Batch>>()
                   .RegisterType<IReadOnlyRepository<BS_Order_SalesOrder_BatchItem>, ReadOnlyRepository<BS_Order_SalesOrder_BatchItem>>()

                   .RegisterType<IRepository<BS_Order_SalesOrder>, Repository<BS_Order_SalesOrder>>()
                   .RegisterType<IRepository<BS_Order_SalesOrder_Batch>, Repository<BS_Order_SalesOrder_Batch>>()
                   .RegisterType<IRepository<BS_Order_SalesOrder_BatchItem>, Repository<BS_Order_SalesOrder_BatchItem>>()

                   ;
        }
    }
}