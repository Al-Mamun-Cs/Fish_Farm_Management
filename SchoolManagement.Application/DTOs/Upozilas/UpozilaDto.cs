using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Upozilas
{
    public class UpozilaDto : IUpozilaDto
    {
        public int UpazilaId { get; set; }
        public int DistrictId { get; set; }
        public string UpazilaName { get; set; }
        public string? UpazilaNameBangla { get; set; }
        public int? Status { get; set; }
        public int? ManuPositon { get; set; }
        public bool IsActive { get; set; }
        public string? District { get; set; }
    }
}
