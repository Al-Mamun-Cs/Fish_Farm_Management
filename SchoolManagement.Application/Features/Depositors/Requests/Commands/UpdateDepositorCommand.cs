using MediatR;
using SchoolManagement.Application.DTOs.Depositors;

namespace SchoolManagement.Application.Features.Depositors.Requests.Commands
{
    public class UpdateDepositorCommand : IRequest<Unit>
    {
        public DepositorDto DepositorDto { get; set; }
    }
}
