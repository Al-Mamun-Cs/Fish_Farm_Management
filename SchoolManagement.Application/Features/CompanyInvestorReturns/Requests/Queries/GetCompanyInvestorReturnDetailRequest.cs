using MediatR;
using SchoolManagement.Application.DTOs.CompanyInvestorReturns;

namespace SchoolManagement.Application.Features.CompanyInvestorReturns.Requests.Queries
{
    public class GetCompanyInvestorReturnDetailRequest : IRequest<CompanyInvestorReturnDto>
    {
        public int CompanyInvestorReturnId { get; set; }
    }
}
