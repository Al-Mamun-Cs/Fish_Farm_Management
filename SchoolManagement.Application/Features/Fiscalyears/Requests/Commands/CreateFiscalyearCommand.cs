using MediatR;
using SchoolManagement.Application.DTOs.Fiscalyears;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Fiscalyears.Requests.Commands
{
    public class CreateFiscalyearCommand : IRequest<BaseCommandResponse>
    {
        public CreateFiscalyearDto FiscalyearDto { get; set; }
    }
}
