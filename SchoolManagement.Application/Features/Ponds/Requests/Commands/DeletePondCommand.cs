using MediatR;

namespace SchoolManagement.Application.Features.Ponds.Requests.Commands
{
    public class DeletePondCommand : IRequest
    {
        public int PondId { get; set; }
    }
}
