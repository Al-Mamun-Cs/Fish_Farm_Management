using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class CashDebit
    {
        public int CashDebitId { get; set; }
        public int? CashInHandId { get; set; }
        public int? WarehouseId { get; set; }
        public decimal? Amount { get; set; }
        public string? Remarks { get; set; }
        public int? ApproveStatus { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual CashInHand? CashInHand { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
    }
}
