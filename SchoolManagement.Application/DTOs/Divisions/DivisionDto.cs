using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Divisions
{
    public class DivisionDto : IDivisionDto
    {
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string? NameBangla { get; set; }
        public int? Status { get; set; }
        public int? ManuPositon { get; set; }
        public bool IsActive { get; set; }
    }
}
