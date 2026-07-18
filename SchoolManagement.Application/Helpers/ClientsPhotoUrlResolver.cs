using AutoMapper;
using SchoolManagement.Domain;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Application.DTOs.Suppliers;

namespace SchoolManagement.Application.Helpers
{
    public class ClientsPhotoUrlResolver : IValueResolver<Supplier, SupplierDto, string>
    {
        

        private readonly IConfiguration _config;
        public ClientsPhotoUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Supplier source, SupplierDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ClientsImage))
            {

                return _config["ApiUrl"] + source.ClientsImage;
            }

            return null;
        }
    }
}
