using Business.Core.Interfaces;
using Service.Contracts.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Contracts;
using Business.Manager.Order.Internal;
using Microsoft.Practices.Unity;
using Business.Manager.Interfaces;
using Business.Manager.Order.Interfaces;

namespace Business.Manager.Order.BizFlowSteps.BMSteps
{
    /// <summary>
    /// Save Order
    /// </summary>
    public class A1_StepSaveOrder : IBizFlowStep<OrderBiz>
    {
        private IManagerCommon _managerCommon;
        private IOrderBatchManager _orderBatchManager;

        public A1_StepSaveOrder(
            IManagerCommon managerCommon,
            IOrderBatchManager orderBatchManager
            )
        {
            this._managerCommon = managerCommon;
            _orderBatchManager = orderBatchManager;
        }

        public string BizType
        {
            get
            {
                return BizTypeEnum.BM.ToString();
            }
        }

        public bool IsEnabled
        {
            get
            {
                return true;
            }
        }

        public bool AllowParallel { get { return false; } }

        public int NextStep
        {
            get
            {
                return 0;//0:auto, -1:end, number:step
            }
        }

        public int StepNumber
        {
            get
            {
                return 10;
            }
        }

        public ResultData<object> ProcessStep(OrderBiz source, object prevResult)
        {
            var result = new ResultData<object>();
            _managerCommon.DoSomeThing();            
            //TODO: do some thing
            _orderBatchManager.UpdateBulk();
            var resultObject = new ProcessObject();
            resultObject.NewOrderCode = Guid.NewGuid().ToString();
            result.Data = resultObject;
            return result; 
        }
    }
}
