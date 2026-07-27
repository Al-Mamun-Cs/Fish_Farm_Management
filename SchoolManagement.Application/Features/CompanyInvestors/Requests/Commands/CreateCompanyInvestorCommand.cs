using MediatR;
using SchoolManagement.Application.DTOs.CompanyInvestors;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.CompanyInvestors.Requests.Commands
{
    public class CreateCompanyInvestorCommand : IRequest<BaseCommandResponse>
    {
        public CreateCompanyInvestorDto CompanyInvestorDto { get; set; }
    }
}
