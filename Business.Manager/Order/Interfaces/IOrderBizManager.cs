using Business.Core.Interfaces;
using Service.Contracts;
using Service.Contracts.ViewModels.Order;

namespace Business.Manager.Order.Interfaces
{
    public interface IOrderBizManager : IBizManager<OrderBiz>
    {
        ResultData<OrderBMResult> ProcessBMOrder(OrderBiz order);
        ResultData<OrderZTResult> ProcessZTOrder(OrderBiz order);
    }
}