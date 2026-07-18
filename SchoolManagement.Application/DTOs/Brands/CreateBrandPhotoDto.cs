using Microsoft.AspNetCore.Http;

namespace SchoolManagement.Application.DTOs.Brands
{
    public class CreateBrandPhotoDto
    {
        

        public IFormFile Photo { get; set; }
        public IFormFile EshopImage { get; set; }
        public CreateBrandDto BrandForm { get; set; }
}
}
