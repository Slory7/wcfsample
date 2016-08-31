using Business.Core.Interfaces;
using Service.Contracts.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Contracts;
using Business.Manager.Order.Internal;

namespace Business.Manager.Order.BizFlowSteps.ZTSteps
{
    /// <summary>
    /// Process PayItem
    /// </summary>
    public class A1_StepPayItem : IBizFlowStep<OrderBiz>
    {
        public string BizType
        {
            get
            {
                return BizTypeEnum.ZT.ToString();
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
                return 1;
            }
        }

        public ResultData<object> ProcessStep(OrderBiz source, object prevResult)
        {
            var result = new ResultData<object>();
            //TODO: do some thing
            var resultObject = new ProcessObject();
            result.Result = resultObject;
            return result; 
        }
    }
}
