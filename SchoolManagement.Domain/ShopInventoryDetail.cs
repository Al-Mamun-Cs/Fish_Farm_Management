using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class ShopInventoryDetail
    {
        public ShopInventoryDetail()
        {
            //ShopInventoryOuts = new HashSet<ShopInventoryOut>();
            
        }

        public int ShopInventoryDetailId { get; set; }
        public int? ShopInventoryId { get; set; }
        public int? WarehouseId { get; set; }
        public int? FisheriesProductTypeId { get; set; }
        public int? FisheriesUnitId { get; set; }
        public string? ProductName { get; set; }
        public decimal? LessAmount { get; set; }
        public decimal? TransportCost { get; set; }
        public decimal? CostingPrice { get; set; }
        public decimal? TotalUnitQty { get; set; }
        public decimal? UnitPurchasePrice { get; set; }
        public decimal? TotalUnitPurchasePrice { get; set; }
        public decimal? AvailableQty { get; set; }
        public decimal? DamageQty { get; set; }
        public decimal? ReturnQty { get; set; }

        public virtual ShopInventory? ShopInventory { get; set; } = null!;
        public virtual Warehouse? Warehouse { get; set; } = null!;
        public virtual FisheriesProductType? FisheriesProductType { get; set; } = null!;
        public virtual FisheriesUnit? FisheriesUnit { get; set; } = null!;

        //public virtual ICollection<ShopInventoryOut> ShopInventoryOuts { get; set; }

    }
}
