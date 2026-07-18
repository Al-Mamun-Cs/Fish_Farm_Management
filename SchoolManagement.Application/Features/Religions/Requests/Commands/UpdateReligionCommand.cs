using MediatR;
using SchoolManagement.Application.DTOs.Religions;

namespace SchoolManagement.Application.Features.Religions.Requests.Commands
{
    public class UpdateReligionCommand : IRequest<Unit>
    {
        public ReligionDto ReligionDto { get; set; }
    }
}
