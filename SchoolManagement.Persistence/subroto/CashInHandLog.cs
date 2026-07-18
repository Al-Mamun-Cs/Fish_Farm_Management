using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class CashInHandLog
    {
        public int CashInHandLogId { get; set; }
        public int CashInHandId { get; set; }
        public int? WarehouseId { get; set; }
        public int? PaymentStatusId { get; set; }
        public decimal? ReceiveAmount { get; set; }
        public decimal? AvailableAmount { get; set; }
        public string? ChequeNo { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public int? BankInfoId { get; set; }
        public string? Remarks { get; set; }
        public int? ApproveStatus { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EasyBikeBank? BankInfo { get; set; }
        public virtual CashInHand CashInHand { get; set; } = null!;
        public virtual PaymentStatus? PaymentStatus { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
    }
}
