using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.DepositorInstallments
{
    public class CreateDepositorInstallmentDto : IDepositorInstallmentDto
    {
        public int DepositorInstallmentId { get; set; }
        public int? WarehouseId { get; set; }
        public int? DepositorId { get; set; }
        public DateTime? InstallmentDate { get; set; }
        public decimal? InstallmentAmount { get; set; }
        public byte? Month { get; set; }
        public short? Year { get; set; }
        public string? Image { get; set; }
        public int? ApproveStatus { get; set; }
        public string? ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }
        public bool IsActive { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
