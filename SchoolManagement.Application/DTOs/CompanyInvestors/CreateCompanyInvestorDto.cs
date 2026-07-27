using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.CompanyInvestors
{
    public class CreateCompanyInvestorDto : ICompanyInvestorDto
    {
        public int CompanyInvestorId { get; set; }
        public int? WarehouseId { get; set; }
        public string? FullName { get; set; }
        public string? ShortName { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime? Date { get; set; }
        public decimal? InvestAmount { get; set; }
        public decimal? ReturnInvestAmount { get; set; }
        public decimal? ProfitAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
