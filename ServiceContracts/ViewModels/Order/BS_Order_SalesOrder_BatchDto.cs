using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.ViewModels.Order
{
    public partial class BS_Order_SalesOrder_BatchDto
    {
        [StringLength(50)]
        public string sBatchCode { get; set; }

        public decimal? dShouldReceive { get; set; }

        public decimal? dRealReceive { get; set; }

        public DateTime? dtSbmmitDate { get; set; }

        [StringLength(50)]
        public string sOpportunityOwner { get; set; }

        public int? nBatchStatus { get; set; }

        public int? nChannel { get; set; }

        [StringLength(50)]
        public string sQuote { get; set; }

        [StringLength(50)]
        public string sOrderCode { get; set; }

        //[StringLength(50)]
        //public string sSources { get; set; }

        //[StringLength(50)]
        //public string sCreatedOperator { get; set; }

        //[StringLength(50)]
        //public string sCenter { get; set; }

        //public bool? bDisposed { get; set; }

        //[StringLength(50)]
        //public string sOperateTypeCode { get; set; }

        //[StringLength(50)]
        //public string sChangedItemCode { get; set; }

        //[StringLength(50)]
        //public string sCrmContactCode { get; set; }

        //[StringLength(50)]
        //public string sBatchGuid { get; set; }

        //public int? nBatchSerial { get; set; }

        public DateTime? dtModify { get; set; }

        //[StringLength(500)]
        //public string sBusinessCode { get; set; }

        public List<BS_Order_SalesOrder_BatchItemDto> BS_Order_SalesOrder_BatchItem { get; set; }

    }
}
