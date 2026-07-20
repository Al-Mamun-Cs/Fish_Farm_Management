using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
   
namespace SchoolManagement.Application.DTOs.ShopGoodSales
{
    public class ShopGoodSaleDetail
    {
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
    }
}
