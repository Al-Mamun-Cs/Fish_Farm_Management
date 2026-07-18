using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Genders
{
    public class CreateGenderDto : IGenderDto
    {
        public int GenderId { get; set; }
        public string? FullName { get; set; }
        public string? ShortName { get; set; }
        public bool IsActive { get; set; }
    }
}
