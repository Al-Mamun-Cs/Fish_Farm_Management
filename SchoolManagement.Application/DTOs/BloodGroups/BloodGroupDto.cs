using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.BloodGroups
{
    public class BloodGroupDto : IBloodGroupDto
    {
        public int BloodGroupId { get; set; }
        public string? FullName { get; set; }
        public string? ShortName { get; set; }
        public bool IsActive { get; set; }
    }
}
