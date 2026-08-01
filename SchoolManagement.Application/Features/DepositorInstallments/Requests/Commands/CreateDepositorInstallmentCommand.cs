using MediatR;
using SchoolManagement.Application.DTOs.DepositorInstallments;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.DepositorInstallments.Requests.Commands
{
    public class CreateDepositorInstallmentCommand : IRequest<BaseCommandResponse>
    {
        public CreateDepositorInstallmentDto DepositorInstallmentDto { get; set; }
    }
}
