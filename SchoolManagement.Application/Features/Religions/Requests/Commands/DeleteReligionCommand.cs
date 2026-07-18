using MediatR;

namespace SchoolManagement.Application.Features.Religions.Requests.Commands
{
    public class DeleteReligionCommand : IRequest
    {
        public int ReligionId { get; set; }
    }
}
