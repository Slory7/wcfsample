using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.ViewModels.Order
{
    public partial class BS_Order_SalesOrderDto
    {
        [StringLength(50)]
        public string sCode { get; set; }

        public int? nChannel { get; set; }

        public DateTime? dtCreateDate { get; set; }

        public DateTime? dtSubmitDate { get; set; }

        public DateTime? dtDisposeDate { get; set; }

        [StringLength(50)]
        public string sCreateOperatorCode { get; set; }

        public decimal? dBalance { get; set; }

        public DateTime? dtEndDate { get; set; }

        public int? nOrderStatus { get; set; }

        public int? nOrderType { get; set; }

        public decimal? dPaymentReceived { get; set; }

        public int? nPayStatus { get; set; }

        //[StringLength(50)]
        //public string sSalesCenter { get; set; }

        //public int? nSchoolId { get; set; }

        //public DateTime? dtStartDate { get; set; }

        //public decimal? dSubReceive { get; set; }

        //public DateTime? dtModify { get; set; }

        //[StringLength(50)]
        //public string sLastVersion { get; set; }

        //[StringLength(50)]
        //public string sOrderGuid { get; set; }

        //[StringLength(50)]
        //public string sEmail { get; set; }

        //[StringLength(50)]
        //public string sE2Code { get; set; }

        //[StringLength(50)]
        //public string sStudentCode { get; set; }

        //public decimal dAccountFee { get; set; }
        public List<BS_Order_SalesOrder_BatchDto> BS_Order_SalesOrder_Batch { get; set; }

    }
}
