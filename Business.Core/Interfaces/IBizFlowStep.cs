using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Interfaces
{
    public interface IBizFlowStep<TEntity>
    {
        int StepNumber { get; }
        string BizType { get; }
        bool IsEnabled { get; }
        bool AllowParallel { get; }
        int NextStep { get; }
        ResultData<object> ProcessStep(TEntity source, object prevResult);
    }
}
