using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.ProjectTypes
{
    public class CreateProjectTypeDto : IProjectTypeDto
    {
        public int ProjectTypeId { get; set; }
        public string? NameEnglish { get; set; }
        public string? NameBangla { get; set; }
        public int? Status { get; set; }
        public int? ManuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
