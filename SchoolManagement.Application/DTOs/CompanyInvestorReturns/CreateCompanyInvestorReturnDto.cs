using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.CompanyInvestorReturns
{
    public class CreateCompanyInvestorReturnDto : ICompanyInvestorReturnDto
    {
        public int CompanyInvestorReturnId { get; set; }
        public int? WarehouseId { get; set; }
        public int? CompanyInvestorId { get; set; }
        public int? PaymentStatusId { get; set; }
        public int? Type { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
        public string? Remarks { get; set; }
        public int? ApproveStatus { get; set; }
        public string? ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }
        public bool IsActive { get; set; }
    }
}
