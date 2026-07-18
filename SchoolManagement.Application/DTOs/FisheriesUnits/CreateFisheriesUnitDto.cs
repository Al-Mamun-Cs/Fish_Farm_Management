using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.FisheriesUnits
{
    public class CreateFisheriesUnitDto : IFisheriesUnitDto
    {
        public int FisheriesUnitId { get; set; }
        public string? FullName { get; set; }
        public string? ShortName { get; set; }
        public bool IsActive { get; set; }
    }
}
