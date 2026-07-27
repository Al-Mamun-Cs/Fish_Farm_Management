using MediatR;
using SchoolManagement.Application.DTOs.CompanyInvestors;

namespace SchoolManagement.Application.Features.CompanyInvestors.Requests.Queries
{
    public class GetCompanyInvestorDetailRequest : IRequest<CompanyInvestorDto>
    {
        public int CompanyInvestorId { get; set; }
    }
}
