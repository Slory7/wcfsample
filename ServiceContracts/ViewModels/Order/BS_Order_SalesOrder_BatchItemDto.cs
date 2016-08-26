using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Contracts.ViewModels.Order
{
    public partial class BS_Order_SalesOrder_BatchItemDto
    {
        [StringLength(50)]
        public string sItemCode { get; set; }

        [StringLength(50)]
        public string sOrderCode { get; set; }

        [StringLength(50)]
        public string sBatchCode { get; set; }

        [StringLength(50)]
        public string sStudentCode { get; set; }

        [StringLength(50)]
        public string sCrmStudentCode { get; set; }

        [StringLength(50)]
        public string sProductType { get; set; }

        [StringLength(50)]
        public string sClassCode { get; set; }

        //public int? nChargeUnit { get; set; }

        //public int? nClassHours { get; set; }

        //public decimal? dStandardPrice { get; set; }

        //public decimal? dSalePrice { get; set; }

        //public decimal? dAmountReceivable { get; set; }

        //public decimal? dPaymentReceived { get; set; }

        //public int? nCourseQuantity { get; set; }

        //public int? nGiftLessonHour { get; set; }

        //[StringLength(50)]
        //public string sOldItemCode { get; set; }

        //[StringLength(50)]
        //public string sOrderOwner { get; set; }

        //public int? nPayStatus { get; set; }

        //public bool? bIsDeleted { get; set; }

        //public int? nPassLesson { get; set; }

        //public decimal? dFee { get; set; }

        //[StringLength(50)]
        //public string sCreateOperator { get; set; }

        //[StringLength(50)]
        //public string sLastModifyOperator { get; set; }

        //public DateTime? dtCreateDate { get; set; }

        //public DateTime? dtModify { get; set; }

        //public bool? bSpecalApprove { get; set; }

        //public DateTime? dtStartDate { get; set; }

        //public DateTime? dtEndDate { get; set; }

        //[StringLength(250)]
        //public string sVersionCode { get; set; }

        //public decimal? dBookFee { get; set; }

        //public decimal? dPenaltyFee { get; set; }

        //public decimal? dPaymentFee { get; set; }

        //public bool? bDisposed { get; set; }

        //[StringLength(50)]
        //public string sCardCode { get; set; }

        //[StringLength(50)]
        //public string sClassTypeFCode { get; set; }

        //public bool? bCardValid { get; set; }

        //public int nLockHour { get; set; }

        //public decimal? dBasicClassFee { get; set; }

        //public int? nInsertLesson { get; set; }

        //public int? nLeaveLesson { get; set; }

        //[StringLength(50)]
        //public string sCourseCode { get; set; }

        //[StringLength(200)]
        //public string sCourseName { get; set; }

        //[StringLength(500)]
        //public string sBusinessItemCode { get; set; }

        //[StringLength(80)]
        //public string sTranCancelReasonType { get; set; }

        //[StringLength(80)]
        //public string sTranCancelReasonItem { get; set; }

        //[StringLength(500)]
        //public string sComment { get; set; }

        //[StringLength(60)]
        //public string sOperateTypeCode { get; set; }
    }
}
