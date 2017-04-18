using PetaPoco;
using Repository.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.Entities.Models
{
    public partial class BS_Order_SalesOrder : Entity
    {
        internal object sCode;

        [ResultColumn]
        public int id { get; set; }

        [ResultColumn]
        public int BatchCount { get; set; }

        public List<BS_Order_SalesOrder_Batch> BS_Order_SalesOrder_Batch { get; set; }
    }   
}