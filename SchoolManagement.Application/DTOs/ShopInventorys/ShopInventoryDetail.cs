using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
   
namespace SchoolManagement.Application.DTOs.ShopInventorys
{
    public class ShopInventoryDetail
    {
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
    }
}
