using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class CashInHand
    {
        public CashInHand()
        {
            CashDebits = new HashSet<CashDebit>();
            CashInHandLogs = new HashSet<CashInHandLog>();
        }

        public int CashInHandId { get; set; }
        public int? WarehouseId { get; set; }
        public int? PaymentStatusId { get; set; }
        public decimal? AvailableAmount { get; set; }
        public int? ApproveStatus { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual PaymentStatus? PaymentStatus { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
        public virtual ICollection<CashDebit> CashDebits { get; set; }
        public virtual ICollection<CashInHandLog> CashInHandLogs { get; set; }
    }
}
