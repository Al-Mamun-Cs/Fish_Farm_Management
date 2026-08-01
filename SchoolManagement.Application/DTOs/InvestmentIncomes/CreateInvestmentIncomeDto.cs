using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.InvestmentIncomes
{
    public class CreateInvestmentIncomeDto : IInvestmentIncomeDto
    {
        public int InvestmentIncomeId { get; set; }
        public int? WarehouseId { get; set; }
        public int? DepositorInvestmentId { get; set; }
        public int? Type { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
        public int? ApproveStatus { get; set; }
        public string? ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }
        public bool IsActive { get; set; }
    }
}
