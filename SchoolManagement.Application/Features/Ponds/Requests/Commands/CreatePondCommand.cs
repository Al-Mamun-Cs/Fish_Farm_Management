using MediatR;
using SchoolManagement.Application.DTOs.Ponds;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Ponds.Requests.Commands
{
    public class CreatePondCommand : IRequest<BaseCommandResponse>
    {
        public CreatePondDto PondDto { get; set; }
    }
}
