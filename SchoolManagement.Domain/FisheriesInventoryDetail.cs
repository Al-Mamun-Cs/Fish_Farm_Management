using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class FisheriesInventoryDetail
    {
        public FisheriesInventoryDetail()
        {
            FisheriesInventoryOuts = new HashSet<FisheriesInventoryOut>();
            
        }

        public int FisheriesInventoryDetailId { get; set; }
        public int? FisheriesInventoryId { get; set; }
        public int? WarehouseId { get; set; }
        public int? FisheriesProductTypeId { get; set; }
        public int? FisheriesUnitId { get; set; }
        public string? ProductName { get; set; }
        public decimal? PurchaseUnit { get; set; }
        public decimal? UnitQty { get; set; }
        public decimal? TotalUnitQty { get; set; }
        public decimal? UnitPurchasePrice { get; set; }
        public decimal? TotalUnitPurchasePrice { get; set; }
        public decimal? AvailableQty { get; set; }
        public decimal? DamageQty { get; set; }
        public decimal? ReturnQty { get; set; }

        public virtual FisheriesInventory? FisheriesInventory { get; set; } = null!;
        public virtual FisheriesProductType? FisheriesProductType { get; set; } = null!;
        public virtual FisheriesUnit? FisheriesUnit { get; set; } = null!;

        public virtual ICollection<FisheriesInventoryOut> FisheriesInventoryOuts { get; set; }

    }
}
