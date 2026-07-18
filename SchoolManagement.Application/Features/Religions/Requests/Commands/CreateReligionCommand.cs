using MediatR;
using SchoolManagement.Application.DTOs.Religions;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Religions.Requests.Commands
{
    public class CreateReligionCommand : IRequest<BaseCommandResponse>
    {
        public CreateReligionDto ReligionDto { get; set; }
    }
}
