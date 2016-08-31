using Service.Contracts;
using Service.Contracts.ViewModels.Order;

namespace Business.Manager.Order.Interfaces
{
    public interface IOrderBizManager
    {
        ResultData<OrderBMResult> ProcessBMOrder(OrderBiz order);
        ResultData<OrderZTResult> ProcessZTOrder(OrderBiz order);
    }
}