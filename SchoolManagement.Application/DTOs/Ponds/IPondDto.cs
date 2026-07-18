using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Ponds
{
    public interface IPondDto
    {
        public int PondId { get; set; }
        public string? NameEnglish { get; set; }
        public string? NameBangla { get; set; }
        public bool IsActive { get; set; }
    } 
}
