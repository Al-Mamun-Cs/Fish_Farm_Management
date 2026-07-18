using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Banks
{
    public class BankDto : IBankDto
    {
        public int BankId { get; set; }
        public string? BankName { get; set; }
        public bool IsActive { get; set; }
    }
}
