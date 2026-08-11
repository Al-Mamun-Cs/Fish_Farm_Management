using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.FisheriesProductReturns
{
    public class CreateFisheriesProductReturnDto : IFisheriesProductReturnDto
    {
        public int FisheriesProductReturnId { get; set; }
        public int? WarehouseId { get; set; }
        public int? SupplierId { get; set; }
        public int? FisheriesProductTypeId { get; set; }
        public int? FisheriesInventoryDetailId { get; set; }
        public int? FisheriesInventoryId { get; set; }
        public int? PaymentReturnType { get; set; }
        public DateTime? Date { get; set; }
        public decimal? ReturnQty { get; set; }
        public decimal? ReturnAmount { get; set; }
        public decimal? ActualReturnValue { get; set; }
        public decimal? DepreciationValue { get; set; }
        public string? Remarks { get; set; }
        public int? ApproveStatus { get; set; }
        public string? ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }
        public bool IsActive { get; set; }
    }
}
