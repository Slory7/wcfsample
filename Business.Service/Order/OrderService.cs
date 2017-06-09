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
using Framework.Core.Net.Http;
using Business.Manager.ExternalApi.WebApi1;
using System.Threading.Tasks;

namespace Business.Service.Order
{
    [HostService]
    [ValidateDataAnnotationsBehavior]
    public class OrderService : IOrderService
    {
        readonly IOrderBizManager _manager;
        readonly IWebApi1Services _webApiService;
        public OrderService(IOrderBizManager manager
            , IWebApi1Services webApiService
            )
        {
            _manager = manager;
            _webApiService = webApiService;
        }

        public ResultData<OrderBMResult> ProcessBMOrder(OrderBiz order)
        {
            ResultData<OrderBMResult> result = null;
            if (!_manager.HasPermission())
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
            if (!_manager.HasPermission())
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

        public async Task<ResultData> GetVoucher()
        {
            ResultData result = new ResultData();
            if (!_manager.HasPermission())
            {
                result.Status = ResultStatus.Unauthorized;
            }
            else
            {
                var httpResult = await _webApiService.GetVoucher();
                result.Status = httpResult.Status;
                result.Message = httpResult.Message;
            }

            return result;
        }

    }
}
