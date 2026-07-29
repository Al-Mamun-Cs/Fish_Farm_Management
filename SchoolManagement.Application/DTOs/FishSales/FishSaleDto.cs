using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.FishSales
{
    public class FishSaleDto : IFishSaleDto
    {
        public int FishSaleId { get; set; }
        public int? WarehouseId { get; set; }
        public int? PondId { get; set; }
        public int? SupplierId { get; set; }
        public int? FisheriesUnitId { get; set; }
        public int? PaymentStatusId { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal? SaleQty { get; set; }
        public decimal? UnitSalePrice { get; set; }
        public decimal? TotalSalePrice { get; set; }
        public decimal? SalePaidAmount { get; set; }
        public decimal? SaleDueAmount { get; set; }
        public bool IsActive { get; set; }

        public string? Warehouse { get; set; }
        public string? Pond { get; set; }
        public string? Supplier { get; set; }
        public string? FisheriesUnit { get; set; }
        public string? PaymentStatus { get; set; }
    }
}
