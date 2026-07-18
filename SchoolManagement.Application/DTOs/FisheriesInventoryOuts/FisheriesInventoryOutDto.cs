using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.FisheriesInventoryOuts
{
    public class FisheriesInventoryOutDto : IFisheriesInventoryOutDto
    {
        public int FisheriesInventoryOutId { get; set; }
        public int? WarehouseId { get; set; }
        public int? PondId { get; set; }
        public int? FisheriesProductTypeId { get; set; }
        public int? FisheriesInventoryDetailId { get; set; }
        public DateTime? Date { get; set; }
        public decimal? UseQty { get; set; }
        public decimal? UnitPurchasePrice { get; set; }
        public bool? ApproveStatus { get; set; }
        public bool IsActive { get; set; }

        public string? Warehouse { get; set; }
        public string? Pond { get; set; }
        public string? ProductName { get; set; }
        public string? ProductType { get; set; }
    }
}
