using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.ViewModels.Order
{
    public class OrderBMResult
    {
        public string NewOrderCode { get; set; }
    }
    public class OrderZTResult
    {
        public int OrderZTTotal { get; set; }
    }
}
