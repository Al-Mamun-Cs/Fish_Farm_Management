using MediatR;
using SchoolManagement.Application.DTOs.CompanyInvestorReturns;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.CompanyInvestorReturns.Requests.Commands
{
    public class CreateCompanyInvestorReturnCommand : IRequest<BaseCommandResponse>
    {
        public CreateCompanyInvestorReturnDto CompanyInvestorReturnDto { get; set; }
    }
}
