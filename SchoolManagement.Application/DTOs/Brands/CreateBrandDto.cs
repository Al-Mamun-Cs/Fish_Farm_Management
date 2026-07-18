using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.Brands
{
    public class CreateBrandDto : IBrandDto
    {
        public int BrandId { get; set; }
        public string? FullName { get; set; }
        public string? ShortName { get; set; }
        public string? BrandImages { get; set; }
        public string? EshopImages { get; set; }
        public bool? IsEshop { get; set; }
        public bool IsActive { get; set; }
        public IFormFile? Photo { get; set; }
        public IFormFile? EshopImage { get; set; }
    }
}
