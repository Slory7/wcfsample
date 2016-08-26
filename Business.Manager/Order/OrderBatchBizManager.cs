﻿using Business.Core.BaseManager;
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

namespace Business.Manager.Order
{
    public class OrderBatchBizManager : BaseBizManager<BS_Order_SalesOrder_Batch>, IOrderBatchBizManager
    {
        IReadOnlyRepository<BS_Order_SalesOrder_Batch> _repositoryReadOnly;
        IRepository<BS_Order_SalesOrder_Batch> _repository;
        IUnitOfWork _unitOfWork;
        IManagerCommon _managerCommon;
        public OrderBatchBizManager(IReadOnlyRepository<BS_Order_SalesOrder_Batch> repositoryReadOnly
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
                return "BS_Order_SalesOrder_Batch";
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
            result.Result = list;

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
                    result.Result = 0;
                    break;
                }
                else result.Result += 1;
            }
            if (result.Status == ResultStatus.Success)
                _unitOfWork.Commit();
            else
                _unitOfWork.Rollback();

            return result;
        }
    }
}