using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.ProjectSchedules
{
    public class CreateProjectScheduleDto : IProjectScheduleDto
    {
        public int ProjectScheduleId { get; set; }
        public int? WarehouseId { get; set; }
        public int? PondId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? ActiveStatus { get; set; }
        public bool IsActive { get; set; }
    }
}
