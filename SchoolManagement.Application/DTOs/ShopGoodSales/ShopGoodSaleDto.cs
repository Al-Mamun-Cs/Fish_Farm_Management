using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.ShopGoodSales
{
    public class ShopGoodSaleDto : IShopGoodSaleDto
    {
        public int ShopGoodSaleId { get; set; }
        public int? WarehouseId { get; set; }
        public int? SupplierId { get; set; }
        public int? PaymentStatusId { get; set; }
        public string? VoucherNo { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal? TotalSalePrice { get; set; }
        public decimal? SaleLessAmount { get; set; }
        public decimal? GrandTotalSalePrice { get; set; }
        public decimal? CustomerPaidAmount { get; set; }
        public decimal? CustomerDueAmount { get; set; }
        public string? Remarks { get; set; }
        public int? ApproveStatus { get; set; }
        public bool IsActive { get; set; }

        public int ShopGoodSaleDetailId { get; set; }
        public int? FisheriesProductTypeId { get; set; }
        public int? FisheriesUnitId { get; set; }
        public int? ShopInventoryDetailId { get; set; }
        public decimal? SaleQty { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? RowTotalSalePrice { get; set; }
        public decimal? CostingPrice { get; set; }
        public decimal? UnitPurchasePrice { get; set; }
        public decimal? Profit { get; set; }

        public string? Warehouse { get; set; }
        public string? Customer { get; set; }
        public string? ProductType { get; set; }
        public string? Unit { get; set; }
        public string? PaymentStatus { get; set; }
        public List<ShopGoodSaleDetailDto>? ShopGoodSaleDetail { get; internal set; }
    }
}
