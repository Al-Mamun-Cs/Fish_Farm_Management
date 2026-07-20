using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public partial class ShopGoodSaleDetail
    {
        public ShopGoodSaleDetail()
        {
            //ShopGoodSaleOuts = new HashSet<ShopGoodSaleOut>();
            
        }

        public int ShopGoodSaleDetailId { get; set; }
        public int? WarehouseId { get; set; }
        public int? ShopGoodSaleId { get; set; }
        public int? FisheriesProductTypeId { get; set; }
        public int? FisheriesUnitId { get; set; }
        public int? ShopInventoryDetailId { get; set; }
        public decimal? SaleQty { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? RowTotalSalePrice { get; set; }
        public decimal? CostingPrice { get; set; }
        public decimal? UnitPurchasePrice { get; set; }
        public decimal? Profit { get; set; }

        public virtual ShopGoodSale? ShopGoodSale { get; set; } = null!;
        public virtual Warehouse? Warehouse { get; set; } = null!;
        public virtual FisheriesProductType? FisheriesProductType { get; set; } = null!;
        public virtual FisheriesUnit? FisheriesUnit { get; set; } = null!;

        //public virtual ICollection<ShopGoodSaleOut> ShopGoodSaleOuts { get; set; }

    }
}
