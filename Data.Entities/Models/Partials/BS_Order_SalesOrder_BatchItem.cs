using PetaPoco;
using Repository.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.Entities.Models
{
    public partial class BS_Order_SalesOrder_BatchItem : Entity
    {
        internal object sItemCode;

        [ResultColumn]
        public int id { get; set; }

        public List<BS_Order_SalesOrder_Voucher> BS_Order_SalesOrder_Voucher { get; set; }
    }   
}