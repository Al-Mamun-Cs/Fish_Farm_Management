using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.ShopGoodSales
{
    public class CreateShopGoodSaleDto : IShopGoodSaleDto
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
    }
}
