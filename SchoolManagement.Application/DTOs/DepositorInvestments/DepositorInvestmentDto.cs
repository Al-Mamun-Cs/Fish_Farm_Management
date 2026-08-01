using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.DepositorInvestments
{
    public class DepositorInvestmentDto : IDepositorInvestmentDto
    {
        public int DepositorInvestmentId { get; set; }
        public int? WarehouseId { get; set; }
        public int? DepositorId { get; set; }
        public DateTime? InvestmenDate { get; set; }
        public decimal? InvestmenAmount { get; set; }
        public decimal? PrincipalReturn { get; set; }
        public decimal? Profit { get; set; }
        public string? BusinessOperatorName { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
        public string? Remarks { get; set; }
        public int? ApproveStatus { get; set; }
        public string? ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }
        public bool IsActive { get; set; }

        public string? Warehouse { get; set; }
        public string? Depositor { get; set; }
    }
}
