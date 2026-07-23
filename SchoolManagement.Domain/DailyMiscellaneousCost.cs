using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class DailyMiscellaneousCost : BaseDomainEntity
    {
        public DailyMiscellaneousCost()
        {
            //FisheriesInventoryDetails = new HashSet<FisheriesInventoryDetail>();
            
        }

        public int DailyMiscellaneousCostId { get; set; }
        public int? WarehouseId { get; set; }
        public int? DailyCostVaucherReasonId { get; set; }
        public int? EmpolyeeId { get; set; }
        public int? PaymentStatusId { get; set; }
        public int? TransactionType { get; set; }
        public int? SupplierId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal? Amount { get; set; }
        public string? Remarks { get; set; }
        public int? ApprovedStatus { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual Warehouse? Warehouse { get; set; }
        public virtual DailyCostVaucherReason? DailyCostVaucherReason { get; set; }
        public virtual PaymentStatus? PaymentStatus { get; set; }
        public virtual Supplier? Supplier { get; set; }

        //public virtual ICollection<FisheriesInventoryDetail> FisheriesInventoryDetails { get; set; }

    }
}
