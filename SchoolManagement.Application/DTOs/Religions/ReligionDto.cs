using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Religions
{
    public class ReligionDto : IReligionDto
    {
        public int ReligionId { get; set; }
        public string? FullName { get; set; }
        public string? ShortName { get; set; }
        public bool IsActive { get; set; }
    }
}
