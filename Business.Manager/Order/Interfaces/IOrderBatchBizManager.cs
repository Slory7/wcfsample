using System.Collections.Generic;
using Service.Contracts;
using Service.Contracts.ViewModels;
using System;
using Data.Entities.Models;
using Service.Contracts.ViewModels.Order;

namespace Business.Manager.Order.Interfaces
{
    public interface IOrderBatchBizManager
    {
        ResultData<List<BS_Order_SalesOrder_BatchDto>> GetOneDayBatch(DateTime day);
        ResultData<int> InsertBulk(ICollection<BS_Order_SalesOrder_Batch> items);
    }
}