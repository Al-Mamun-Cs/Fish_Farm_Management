using MediatR;
using SchoolManagement.Application.DTOs.DepositorInvestments;

namespace SchoolManagement.Application.Features.DepositorInvestments.Requests.Commands
{
    public class UpdateDepositorInvestmentCommand : IRequest<Unit>
    {
        public DepositorInvestmentDto DepositorInvestmentDto { get; set; }
    }
}
