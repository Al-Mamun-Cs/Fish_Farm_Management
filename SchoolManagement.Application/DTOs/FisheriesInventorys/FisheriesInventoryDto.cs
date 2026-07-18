using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.FisheriesInventorys
{
    public class FisheriesInventoryDto : IFisheriesInventoryDto
    {
        public int FisheriesInventoryId { get; set; }
        public int? WarehouseId { get; set; }
        public int? SupplierId { get; set; }
        public int? PaymentStatusId { get; set; }
        public string? VoucherNo { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? LessAmount { get; set; }
        public decimal? TransportCost { get; set; }
        public decimal? TotalPurchasePrice { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? DueAmount { get; set; }
        public bool? ApproveStatus { get; set; }

        public int FisheriesInventoryDetailId { get; set; }
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
        public bool IsActive { get; set; }

        public string? Warehouse { get; set; }
        public string? Supplier { get; set; }
        public string? ProductType { get; set; }
        public string? Unit { get; set; }
        public string? PaymentStatus { get; set; }
        public List<FisheriesInventoryDetailDto>? FisheriesInventoryDetail { get; internal set; }
    }
}
