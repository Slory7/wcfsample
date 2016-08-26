using Service.Contracts.ViewModels;
using Service.Contracts.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service.Contracts.Services.Order
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        ResultData<List<BS_Order_SalesOrder_BatchDto>> GetBatchsByCodes(string batchCodes);

        [OperationContract]
        ResultData<List<BS_Order_SalesOrderDto>> GetOneDayOrder(DateTime day);

        [OperationContract]
        ResultData<List<BS_Order_SalesOrder_BatchDto>> GetOneDayBatch(DateTime day);

        [OperationContract]
        ResultData<BS_Order_SalesOrder_BatchDto> UpdateBatch(BS_Order_SalesOrder_BatchDto item);

        [OperationContract]
        ResultData<List<BS_Order_SalesOrder_BatchDto>> InsertBulk(List<BS_Order_SalesOrder_BatchDto> items);

        [OperationContract]
        ResultData<int> Delete(object primaryKey);
        // TODO: 在此添加您的服务操作
    }

}
