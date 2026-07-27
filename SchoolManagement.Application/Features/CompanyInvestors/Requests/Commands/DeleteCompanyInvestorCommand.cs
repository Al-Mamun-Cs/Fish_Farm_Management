using MediatR;

namespace SchoolManagement.Application.Features.CompanyInvestors.Requests.Commands
{
    public class DeleteCompanyInvestorCommand : IRequest
    {
        public int CompanyInvestorId { get; set; }
    }
}
