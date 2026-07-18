using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
   
namespace SchoolManagement.Application.DTOs.FisheriesInventorys
{
    public class FisheriesInventoryDetail
    {
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
    }
}
