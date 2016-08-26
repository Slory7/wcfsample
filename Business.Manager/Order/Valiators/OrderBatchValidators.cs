using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Service.Contracts;
using Business.Core;
using Data.Entities.Models;
using Microsoft.Practices.Unity;
using Business.Manager.Order.Interfaces;
using Business.Core.Interfaces;
using Service.Core;

namespace Business.Manager.Order.Valiators
{
    public class OrderBatchValidators
    {
        internal class OrderBatchValidator1 : IDataValidator<BS_Order_SalesOrder_Batch>
        {
            public int Order
            {
                get
                {
                    return 1;
                }
            }
            bool? _isEnabled;
            public bool IsEnabled
            {
                get
                {
                    if (_isEnabled == null)
                    {
                        //TODO:Get setting from bs_config
                        _isEnabled = true;
                    }
                    return _isEnabled.Value;
                }
            }
            public string CheckValid(BS_Order_SalesOrder_Batch item, DBOperation operation)
            {
                var orderCommon = ServiceGlobals.UnityContainer.Resolve<IOrderCommon>();
                orderCommon.DoSomeThing();

                string errorMessage = null;
                bool isValid = (operation != DBOperation.Insert && operation != DBOperation.Update)
                                || (item.dRealReceive > 0);
                if (!isValid) errorMessage = "费用不符合要求";
                else
                {
                    isValid = (operation != DBOperation.Insert)
                              || (item.nChannel > -1);
                    if (!isValid) errorMessage = "渠道不符合要求";
                }

                return errorMessage;
            }
        }
        internal class OrderBatchValidator2 : IDataValidator<BS_Order_SalesOrder_Batch>
        {
            public int Order
            {
                get
                {
                    return 2;
                }
            }
            bool? _isEnabled;
            public bool IsEnabled
            {
                get
                {
                    if (_isEnabled == null)
                    {
                        //TODO:Get setting from bs_config
                        _isEnabled = true;
                    }
                    return _isEnabled.Value;
                }
            }
            public string CheckValid(BS_Order_SalesOrder_Batch item, DBOperation operation)
            {
                bool isInValid = operation == DBOperation.Insert && item.sBusinessCode != null;
                string errorMessage = isInValid ? "新增时特定学校BusinessCode不符合要求" : null;
                return errorMessage;
            }
        }
    }
}