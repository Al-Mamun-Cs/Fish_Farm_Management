using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Depositors
{
    public class CreateDepositorDto : IDepositorDto
    {
        public int DepositorId { get; set; }
        public int? WarehouseId { get; set; }
        public string? DepositorName { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public decimal? PresentBalance { get; set; }
        public bool IsActive { get; set; }
    }
}
