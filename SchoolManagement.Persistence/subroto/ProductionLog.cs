using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.subroto
{
    public partial class ProductionLog
    {
        public int ProductionLogId { get; set; }
        public int? InventoryId { get; set; }
        public int? ProductTypeId { get; set; }
        public int? CategoryId { get; set; }
        public decimal? UseQty { get; set; }
        public decimal? ProductionQty { get; set; }
        public int? ApproveStatus { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual EasyBikeType? Category { get; set; }
        public virtual Inventory? Inventory { get; set; }
        public virtual ProductType? ProductType { get; set; }
    }
}
