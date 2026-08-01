using MediatR;

namespace SchoolManagement.Application.Features.Depositors.Requests.Commands
{
    public class DeleteDepositorCommand : IRequest
    {
        public int DepositorId { get; set; }
    }
}
