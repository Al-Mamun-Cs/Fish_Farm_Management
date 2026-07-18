using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class FisheriesInventory : BaseDomainEntity
    {
        public FisheriesInventory()
        {
            FisheriesInventoryDetails = new HashSet<FisheriesInventoryDetail>();
            
        }

        public int FisheriesInventoryId { get; set; }
        public int? WarehouseId { get; set; }
        public int? SupplierId { get; set; }
        public int? PaymentStatusId { get; set; }
        public string? VoucherNo { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? LessAmount { get; set; }
        public decimal? TransportCost { get; set; }
        public decimal? TotalPurchasePrice { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? DueAmount { get; set; }
        public bool? ApproveStatus { get; set; }
        public bool IsActive { get; set; }

        public virtual Warehouse? Warehouse { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual PaymentStatus? PaymentStatus { get; set; }

        public virtual ICollection<FisheriesInventoryDetail> FisheriesInventoryDetails { get; set; }

    }
}
