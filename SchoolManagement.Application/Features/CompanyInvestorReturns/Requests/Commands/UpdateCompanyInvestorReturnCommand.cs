using MediatR;
using SchoolManagement.Application.DTOs.CompanyInvestorReturns;

namespace SchoolManagement.Application.Features.CompanyInvestorReturns.Requests.Commands
{
    public class UpdateCompanyInvestorReturnCommand : IRequest<Unit>
    {
        public CompanyInvestorReturnDto CompanyInvestorReturnDto { get; set; }
    }
}
