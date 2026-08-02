using AutoMapper;
using SchoolManagement.Domain;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Application.DTOs.DepositorInstallments;

namespace SchoolManagement.Application.Helpers
{
    public class DepositorInstallmentImageUrlResolver : IValueResolver<DepositorInstallment, DepositorInstallmentDto, string>
    {
        

        private readonly IConfiguration _config;
        public DepositorInstallmentImageUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(DepositorInstallment source, DepositorInstallmentDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Image))
            {

                return _config["ApiUrl"] + source.Image;
            }

            return null;
        }
    }
}
