using AutoMapper;
using Business.Core;
using DevTrends.WCFDataAnnotations;
using Framework.Core;
using Service.Core;
using Service.Contracts;
using Service.Contracts.Services;
using Service.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Business.Manager.Order.Interfaces;
using Data.Entities.Models;
using Service.Contracts.Services.Order;
using Service.Contracts.ViewModels.Order;
using Business.Core.Interfaces;

namespace Business.Service.Order
{
    [HostService]
    [ValidateDataAnnotationsBehavior]
    public class OrderService : IOrderService
    {
        IBizManager<OrderBiz> _bizManager;
        IOrderBizManager  _manager;
        public OrderService(IBizManager<OrderBiz> bizManager
            ,IOrderBizManager manager
            )
        {
            _bizManager = bizManager;
            _manager = manager;
        }

        public ResultData<OrderBMResult> ProcessBMOrder(OrderBiz order)
        {
            ResultData<OrderBMResult> result = null;
            if (!_bizManager.HasPermission())
            {
                result = new ResultData<OrderBMResult>()
                {
                    Status = ResultStatus.Unauthorized
                };
            }
            else
            {
                result = _manager.ProcessBMOrder(order);
            }

            return result;
        }
        public ResultData<OrderZTResult> ProcessZTOrder(OrderBiz order)
        {
            ResultData<OrderZTResult> result = null;
            if (!_bizManager.HasPermission())
            {
                result = new ResultData<OrderZTResult>()
                {
                    Status = ResultStatus.Unauthorized
                };
            }
            else
            {
                result = _manager.ProcessZTOrder(order);
            }

            return result;
        }

    }
}
