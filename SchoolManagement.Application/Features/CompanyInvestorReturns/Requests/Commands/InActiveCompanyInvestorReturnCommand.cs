using MediatR;

namespace SchoolManagement.Application.Features.CompanyInvestorReturns.Requests.Commands
{
    public class InActiveCompanyInvestorReturnCommand : IRequest 
    {
        public int CompanyInvestorReturnId { get; set; }
    }
}
