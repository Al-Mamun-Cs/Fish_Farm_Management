using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Fiscalyears
{
    public class FiscalyearDto : IFiscalyearDto
    {
        public int FiscalyearId { get; set; }
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
