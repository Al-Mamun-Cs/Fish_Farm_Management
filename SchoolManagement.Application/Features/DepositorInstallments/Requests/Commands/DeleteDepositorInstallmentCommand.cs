using MediatR;

namespace SchoolManagement.Application.Features.DepositorInstallments.Requests.Commands
{
    public class DeleteDepositorInstallmentCommand : IRequest
    {
        public int DepositorInstallmentId { get; set; }
    }
}
