using MediatR;

namespace SchoolManagement.Application.Features.CompanyInvestorReturns.Requests.Commands
{
    public class DeleteCompanyInvestorReturnCommand : IRequest
    {
        public int CompanyInvestorReturnId { get; set; }
    }
}
