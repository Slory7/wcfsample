using System.Collections.Generic;
using Service.Contracts;
using Service.Contracts.ViewModels;
using System;
using Data.Entities.Models;
using Service.Contracts.ViewModels.Order;
using Business.Core.Interfaces;

namespace Business.Manager.Order.Interfaces
{
    public interface IOrderBatchManager : IManager<BS_Order_SalesOrder_Batch>
    {
        ResultData<List<BS_Order_SalesOrder_BatchDto>> GetOneDayBatch(DateTime day);
        ResultData<int> InsertBulk(ICollection<BS_Order_SalesOrder_Batch> items);
    }
}