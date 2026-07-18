using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.PaymentStatuses
{
    public class CreatePaymentStatusDto : IPaymentStatusDto
    {
        public int PaymentStatusId { get; set; }
        public string? StatusName { get; set; }
        public int? PriorityNo { get; set; }
        public bool IsActive { get; set; }
    }
}
