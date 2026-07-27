using MediatR;
using SchoolManagement.Application.DTOs.CompanyInvestors;

namespace SchoolManagement.Application.Features.CompanyInvestors.Requests.Commands
{
    public class UpdateCompanyInvestorCommand : IRequest<Unit>
    {
        public CompanyInvestorDto CompanyInvestorDto { get; set; }
    }
}
