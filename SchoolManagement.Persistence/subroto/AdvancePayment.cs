using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class AdvancePayment
    {
        public int AdvancePaymentId { get; set; }
        public int? SupplierId { get; set; }
        public int? WarehouseId { get; set; }
        public int? CategoryId { get; set; }
        public int? PaymentStatusId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? CustomerReaceveDate { get; set; }
        public string? GoodsQty { get; set; }
        public int? AdjustmentStatus { get; set; }
        public string? Remarks { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EasyBikeType? Category { get; set; }
        public virtual PaymentStatus? PaymentStatus { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
    }
}
