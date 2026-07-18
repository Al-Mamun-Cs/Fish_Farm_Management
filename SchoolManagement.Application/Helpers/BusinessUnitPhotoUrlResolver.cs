using AutoMapper;
using SchoolManagement.Domain;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Application.DTOs.Warehouses;

namespace SchoolManagement.Application.Helpers
{
    public class BusinessUnitPhotoUrlResolver : IValueResolver<Warehouse, WarehouseDto, string>
    {
        

        private readonly IConfiguration _config;
        public BusinessUnitPhotoUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Warehouse source, WarehouseDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.BusinessImages))
            {

                return _config["ApiUrl"] + source.BusinessImages;
            }
            if (!string.IsNullOrEmpty(source.ProductImages))
            {

                return _config["ApiUrl"] + source.ProductImages;
            }

            return null;
        }
    }
}
