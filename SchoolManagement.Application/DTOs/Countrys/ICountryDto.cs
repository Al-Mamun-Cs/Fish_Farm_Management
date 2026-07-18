using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Countrys
{
    public interface ICountryDto
    {
        public int CountryId { get; set; }
        public string? Name { get; set; }
        public int? Status { get; set; }
        public bool IsActive { get; set; }
    } 
}
