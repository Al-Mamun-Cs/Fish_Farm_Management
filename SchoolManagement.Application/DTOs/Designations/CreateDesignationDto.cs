using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Designations
{
    public class CreateDesignationDto : IDesignationDto
    {
        public int DesignationId { get; set; }
        public int? WarehouseId { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public int? MenuPosition { get; set; }
        public int? ServiceAge { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
