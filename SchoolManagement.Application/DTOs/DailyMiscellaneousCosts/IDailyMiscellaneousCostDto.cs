using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.DailyMiscellaneousCosts
{
    public interface IDailyMiscellaneousCostDto
    {
        public int DailyMiscellaneousCostId { get; set; }
        public int? WarehouseId { get; set; }
        public int? DailyCostVaucherReasonId { get; set; }
        public int? EmpolyeeId { get; set; }
        public int? PaymentStatusId { get; set; }
        public int? TransactionType { get; set; }
        public int? SupplierId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal? Amount { get; set; }
        public string? Remarks { get; set; }
        public int? ApprovedStatus { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public bool IsActive { get; set; }
    } 
}
