using Repository.Pattern.Infrastructure;
using Repository.Pattern.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Service.Contracts.ViewModels;
using Service.Core;
using Data.Entities.Models.Mapper;
using Data.Entities.Models;
using Service.Contracts.ViewModels.Order;

namespace Data.Repository.Repositories.Order
{
    public static class OrderRepository
    {
        public static List<BS_Order_SalesOrder_BatchDto> GetOneDayBatchs(this IReadOnlyRepository<BS_Order_SalesOrder_Batch> repos, DateTime day)
        {
            var unityOfWorkReadOnly = ServiceGlobals.UnityContainer.Resolve<IReadOnlyUnitOfWork>();
            string strSql = @"Select * from BS_Order_SalesOrder_Batch a inner join BS_Order_SalesOrder_BatchItem b
on a.sBatchCode = b.sBatchCode where a.dtModify between @0 and @1";
            var lists = unityOfWorkReadOnly.Database.Fetch<BS_Order_SalesOrder_BatchDto, BS_Order_SalesOrder_BatchItemDto, BS_Order_SalesOrder_BatchDto>(
                new OrderBatchItemDtoRelator().MapIt,
                strSql,
                day, day.AddDays(1));
            return lists;
        }
        public static List<BS_Order_SalesOrder_BatchDto> BeforeBatch(this IReadOnlyRepository<BS_Order_SalesOrder_Batch> repos)
        {
            string strSql = "Where dtModify<@0";
            var lists = repos.Fetch<BS_Order_SalesOrder_BatchDto>(strSql, DateTime.Today);
            return lists;
        }
    }
}
