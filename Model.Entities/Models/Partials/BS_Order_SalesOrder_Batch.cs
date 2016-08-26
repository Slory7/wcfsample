using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.Entities.Models
{
    public partial class BS_Order_SalesOrder_Batch
    {
        public List<BS_Order_SalesOrder_BatchItem> BS_Order_SalesOrder_BatchItem { get; set; }
    }   
}