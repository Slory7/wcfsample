using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public class ResultData<T> : ResultData where T : new()
    {
        public ResultData()
        {
            this.Status = ResultStatus.Success;
        }
        public T Result { get; set; }
    }
    public class ResultData
    {
        public ResultStatus Status { get; set; }
        public string Message { get; set; }
    }
}
