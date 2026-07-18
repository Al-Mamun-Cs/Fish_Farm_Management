using MediatR;
using SchoolManagement.Application.DTOs.Divisions;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Divisions.Requests.Commands
{
    public class CreateDivisionCommand : IRequest<BaseCommandResponse>
    {
        public CreateDivisionDto DivisionDto { get; set; }
    }
}
