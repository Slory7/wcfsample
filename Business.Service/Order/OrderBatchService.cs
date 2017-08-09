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
    public class OrderBatchService : IOrderBatchService
    {
        IOrderBatchManager _orderBatchBizManager;
        IManager<BS_Order_SalesOrder_Batch> _manager;
        public OrderBatchService(IManager<BS_Order_SalesOrder_Batch> manager
            , IOrderBatchManager orderBatchBizManager
            )
        {
            _manager = manager;
            _orderBatchBizManager = orderBatchBizManager;
        }

        public ResultData<List<BS_Order_SalesOrderDto>> GetOneDayOrder(DateTime day)
        {
            return null;
        }
        public ResultData<List<BS_Order_SalesOrder_BatchDto>> GetBatchsByCodes(string batchCodes)
        {
            var result = new ResultData<List<BS_Order_SalesOrder_BatchDto>>();

            string[] codes = batchCodes.Split(',');
            var objList = new List<BS_Order_SalesOrder_BatchDto>(codes.Length);
            foreach (string code in codes)
            {
                var objEntity = _manager.SingleOrDefault(code);
                if (objEntity != null)
                {
                    var objDto = ConvertToDto(objEntity);
                    objList.Add(objDto);
                }
            }
            result.Data = objList;

            return result;
        }

        public ResultData<List<BS_Order_SalesOrder_BatchDto>> GetOneDayBatch(DateTime day)
        {
            return _orderBatchBizManager.GetOneDayBatch(day);
        }

        public ResultData<List<BS_Order_SalesOrder_BatchDto>> InsertBulk(List<BS_Order_SalesOrder_BatchDto> items)
        {
            var result = new ResultData<List<BS_Order_SalesOrder_BatchDto>>();

            var itemsToInsert = ConvertFromDto(items);

            var resultItems = _orderBatchBizManager.InsertBulk(itemsToInsert);

            if (resultItems.Status == ResultStatus.Success)
            {
                result.Data = ConvertToDto(itemsToInsert).ToList();
            }

            result.Message = resultItems.Message;
            result.Status = resultItems.Status;

            return result;
        }

        public ResultData<BS_Order_SalesOrder_BatchDto> UpdateBatch(BS_Order_SalesOrder_BatchDto item)
        {
            var result = new ResultData<BS_Order_SalesOrder_BatchDto>();

            try
            {
                var itemToUpdate = _manager.SingleOrDefault(item.sBatchCode);

                if (itemToUpdate == null)
                {
                    result.Status = ResultStatus.NotFound;
                }
                else
                {
                    Framework.Core.Reflection.Utils.CopyProperty(item, itemToUpdate);

                    //Change property by logic
                    itemToUpdate.dtModify = DateTime.Now;

                    var resultItem = _manager.Update(itemToUpdate);

                    if (resultItem.Status == ResultStatus.Success)
                    {
                        //affect result
                        item.dtModify = DateTime.Now;
                        result.Data = item;
                    }
                    else
                    {
                        result.Status = resultItem.Status;
                        result.Message = resultItem.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = ResultStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        public ResultData<int> Delete(object primaryKey)
        {
            ResultData<int> result = null;
            if (!_manager.HasPermission())
            {
                result = new ResultData<int>()
                {
                    Status = ResultStatus.Unauthorized
                };
            }
            else
            {
                result = _manager.Delete(primaryKey);
            }
            return result;
        }

        #region Dto Poco Convertor

        public ICollection<BS_Order_SalesOrder_Batch> ConvertFromDto(ICollection<BS_Order_SalesOrder_BatchDto> source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BS_Order_SalesOrder_BatchDto, BS_Order_SalesOrder_Batch>();
                cfg.CreateMap<BS_Order_SalesOrder_BatchItemDto, BS_Order_SalesOrder_BatchItem>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<ICollection<BS_Order_SalesOrder_Batch>>(source);
        }
        public BS_Order_SalesOrder_Batch ConvertFromDto(BS_Order_SalesOrder_BatchDto source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BS_Order_SalesOrder_BatchDto, BS_Order_SalesOrder_Batch>();
                cfg.CreateMap<BS_Order_SalesOrder_BatchItemDto, BS_Order_SalesOrder_BatchItem>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<BS_Order_SalesOrder_Batch>(source);
        }
        public ICollection<BS_Order_SalesOrder_BatchDto> ConvertToDto(ICollection<BS_Order_SalesOrder_Batch> source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BS_Order_SalesOrder_Batch, BS_Order_SalesOrder_BatchDto>();
                cfg.CreateMap<BS_Order_SalesOrder_BatchItem, BS_Order_SalesOrder_BatchItemDto>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<ICollection<BS_Order_SalesOrder_BatchDto>>(source);
        }
        public BS_Order_SalesOrder_BatchDto ConvertToDto(BS_Order_SalesOrder_Batch source)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BS_Order_SalesOrder_Batch, BS_Order_SalesOrder_BatchDto>();
                cfg.CreateMap<BS_Order_SalesOrder_BatchItem, BS_Order_SalesOrder_BatchItemDto>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<BS_Order_SalesOrder_BatchDto>(source);
        }

        #endregion
    }
}
