using Service.Contracts;
using Service.Contracts.ViewModels.Order;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Service.Contracts.Services.Order
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        ResultData<OrderBMResult> ProcessBMOrder(OrderBiz order);
        [OperationContract]
        ResultData<OrderZTResult> ProcessZTOrder(OrderBiz order);
        [OperationContract]
        Task<ResultData> GetVoucher();
    }
}