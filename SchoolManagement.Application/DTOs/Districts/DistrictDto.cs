using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Districts
{
    public class DistrictDto : IDistrictDto
    {
        public int DistrictId { get; set; }
        public int DivisionId { get; set; }
        public string DistrictName { get; set; }
        public string? DistrictNameBangla { get; set; }
        public int? Status { get; set; }
        public int? ManuPositon { get; set; }
        public decimal? ShippingCharge { get; set; }
        public bool IsActive { get; set; }

        public string? Division { get; set; }

    }
}
