using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.EasyBikeBanks
{
    public class EasyBikeBankDto : IEasyBikeBankDto
    {
        public int EasyBikeBankId { get; set; }
        public int? AccountNameId { get; set; }
        public int? AccountTypeId { get; set; }
        public string? BankName { get; set; }
        public string? BankAccountNo { get; set; }
        public string? Address { get; set; }
        public string? PhoneNo { get; set; }
        public decimal? BankBalance { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }

        public string? AccountName { get; set; }
        public string? AccountType { get; set; }
    }
}
