using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.Entities.Models
{
    public partial class BS_Order_SalesOrder
    {
        [ResultColumn]
        public int BatchCount { get; set; }

        public List<BS_Order_SalesOrder_Batch> BS_Order_SalesOrder_Batch { get; set; }
    }   
}