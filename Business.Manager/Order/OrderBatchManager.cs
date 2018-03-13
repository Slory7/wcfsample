using Business.Core.BaseManager;
using Business.Manager.Order.Interfaces;
using Data.Entities.Models;
using Repository.Pattern.Interface;
using Service.Contracts;
using Service.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using Data.Repository.Repositories.Order;
using System.Linq;
using Service.Contracts.ViewModels.Order;
using Business.Manager.Interfaces;
using System.Threading;

namespace Business.Manager.Order
{
    public class OrderBatchManager : BaseManager<BS_Order_SalesOrder_Batch>, IOrderBatchManager
    {
        IReadOnlyRepository<BS_Order_SalesOrder_Batch> _repositoryReadOnly;
        IRepository<BS_Order_SalesOrder_Batch> _repository;
        IUnitOfWork _unitOfWork;
        IManagerCommon _managerCommon;
        public OrderBatchManager(IReadOnlyRepository<BS_Order_SalesOrder_Batch> repositoryReadOnly
            , IRepository<BS_Order_SalesOrder_Batch> repository
            , IUnitOfWork unitOfWork
            , IManagerCommon managerCommon
            ) : base(repositoryReadOnly, repository)
        {
            _repositoryReadOnly = repositoryReadOnly;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _managerCommon = managerCommon;
        }
        public override string InfoName
        {
            get
            {
                return "OrderBatch";
            }
        }

        public override void InitObject(BS_Order_SalesOrder_Batch item)
        {
            _managerCommon.DoSomeThing();

            item.sBatchGuid = Guid.NewGuid().ToString();
            //TODO: GetCode
            item.sBatchCode = Guid.NewGuid().ToString();
            if (item.sOpportunityOwner == null)
                item.sOpportunityOwner = "defaultValue";
        }

        public ResultData<List<BS_Order_SalesOrder_BatchDto>> GetOneDayBatch(DateTime day)
        {
            var list = _repositoryReadOnly.GetOneDayBatchs(day);

            var result = new ResultData<List<BS_Order_SalesOrder_BatchDto>>();
            result.Data = list;

            return result;
        }
        public ResultData<int> UpdateBulk()
        {
            var result = new ResultData<int>();
            using (var tx = _unitOfWork.GetTransactionObject())
            {
                //result.Data = _repository.Update("set sMarketingSourcesExt=null", "");
                //Thread.Sleep(220 * 1000);
            }
            return result;
        }
        public ResultData<int> InsertBulk(ICollection<BS_Order_SalesOrder_Batch> items)
        {
            var result = new ResultData<int>();

            _unitOfWork.BeginTransaction();
            foreach (var objItem in items)
            {
                var resultItem = base.Insert(objItem);
                if (resultItem.Status != ResultStatus.Success)
                {
                    result.Status = resultItem.Status;
                    result.Message = resultItem.Message;
                    result.Data = 0;
                    break;
                }
                else result.Data += 1;
            }
            if (result.Status == ResultStatus.Success)
                _unitOfWork.Commit();
            else
                _unitOfWork.Rollback();

            return result;
        }
    }
}