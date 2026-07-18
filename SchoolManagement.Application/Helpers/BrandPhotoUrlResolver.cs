using AutoMapper;
using SchoolManagement.Domain;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Application.DTOs.Brands;

namespace SchoolManagement.Application.Helpers
{
    public class BrandPhotoUrlResolver : IValueResolver<Brand, BrandDto, string>
    {
        

        private readonly IConfiguration _config;
        public BrandPhotoUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Brand source, BrandDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.BrandImages))
            {

                return _config["ApiUrl"] + source.BrandImages;
            }

            if (!string.IsNullOrEmpty(source.BrandImages))
            {

                return _config["ApiUrl"] + source.EshopImages;
            }

            return null;
        }
    }
}
