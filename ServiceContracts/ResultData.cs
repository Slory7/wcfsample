using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    [DataContract]
    public class ResultData<T> : ResultData where T : new()
    {
        public ResultData()
        {
            this.Status = ResultStatus.Success;
        }
        [DataMember]
        public T Data { get; set; }
    }

    [DataContract]
    public class ResultData
    {
        [DataMember]
        public ResultStatus Status { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}
