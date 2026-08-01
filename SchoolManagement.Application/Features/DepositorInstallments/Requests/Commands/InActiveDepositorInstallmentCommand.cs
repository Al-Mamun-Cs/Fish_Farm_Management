using MediatR;

namespace SchoolManagement.Application.Features.DepositorInstallments.Requests.Commands
{
    public class InActiveDepositorInstallmentCommand : IRequest 
    {
        public int DepositorInstallmentId { get; set; }
    }
}
