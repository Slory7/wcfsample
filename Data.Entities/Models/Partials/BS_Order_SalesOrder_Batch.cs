using PetaPoco;
using Repository.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.Entities.Models
{
    public partial class BS_Order_SalesOrder_Batch : Entity
    {
        public int dRealReceive;
        public int nChannel;
        public string sBusinessCode;
        public string sBatchGuid;
        public string sBatchCode;
        public string sOpportunityOwner;
        public DateTime dtModify;

        [ResultColumn]
        public int id { get; set; }

        public List<BS_Order_SalesOrder_BatchItem> BS_Order_SalesOrder_BatchItem { get; set; }
    }
}