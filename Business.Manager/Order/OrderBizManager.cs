using Business.Core.BaseManager;
using Business.Core.Interfaces;
using Business.Manager.Interfaces;
using Business.Manager.Order.Interfaces;
using Business.Manager.Order.Internal;
using Data.Entities.Models;
using Repository.Pattern.Interface;
using Service.Contracts;
using Service.Contracts.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager.Order
{
    public class OrderBizManager : BaseBizManager<OrderBiz>, IOrderBizManager
    {
        IUnitOfWork _unitOfWork;
        IManagerCommon _managerCommon;
        IManager<BS_Order_SalesOrder_Batch> _managerBatch;
        public OrderBizManager(IManager<BS_Order_SalesOrder_Batch> managerBatch
            , IUnitOfWork unitOfWork
            , IManagerCommon managerCommon
            )
        {
            _managerBatch = managerBatch;
            _unitOfWork = unitOfWork;
            _managerCommon = managerCommon;
        }
        public override string InfoName
        {
            get
            {
                return "OrderBiz";
            }
        }

        public override void InitObject(OrderBiz item)
        {
            //throw new NotImplementedException();
        }
        public ResultData<OrderBMResult> ProcessBMOrder(OrderBiz order)
        {
            var result = new ResultData<OrderBMResult>();

            InitObject(order);

            using (_unitOfWork.GetTransactionObject())
            {
                var resultProcess = ProcessBizFlow(order, BizTypeEnum.BM.ToString());

                if (result.Status == ResultStatus.Success)
                {
                    _unitOfWork.Commit();

                    result.Data = new OrderBMResult()
                    {
                        NewOrderCode = ((ProcessObject)resultProcess.Data).NewOrderCode
                    };
                }
                else
                    _unitOfWork.Rollback();

                result.Status = resultProcess.Status;
                result.Message = resultProcess.Message;
            }
            return result;
        }
        public ResultData<OrderZTResult> ProcessZTOrder(OrderBiz order)
        {
            var result = new ResultData<OrderZTResult>();

            InitObject(order);

            using (_unitOfWork.GetTransactionObject())
            {
                var resultProcess = ProcessBizFlow(order, BizTypeEnum.ZT.ToString());
                if (result.Status == ResultStatus.Success)
                {
                    _unitOfWork.Commit();
                    result.Data = new OrderZTResult()
                    {
                        OrderZTTotal = ((ProcessObject)resultProcess.Data).ZTTotal
                    };
                }
                else
                    _unitOfWork.Rollback();

                result.Status = resultProcess.Status;
                result.Message = resultProcess.Message;
            }
            return result;
        }
    }
}
