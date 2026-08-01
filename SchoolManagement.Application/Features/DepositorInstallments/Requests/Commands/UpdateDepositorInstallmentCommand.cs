using MediatR;
using SchoolManagement.Application.DTOs.DepositorInstallments;

namespace SchoolManagement.Application.Features.DepositorInstallments.Requests.Commands
{
    public class UpdateDepositorInstallmentCommand : IRequest<Unit>
    {
        public DepositorInstallmentDto DepositorInstallmentDto { get; set; }
    }
}
