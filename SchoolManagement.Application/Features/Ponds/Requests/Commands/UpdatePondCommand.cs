using MediatR;
using SchoolManagement.Application.DTOs.Ponds;

namespace SchoolManagement.Application.Features.Ponds.Requests.Commands
{
    public class UpdatePondCommand : IRequest<Unit>
    {
        public PondDto PondDto { get; set; }
    }
}
