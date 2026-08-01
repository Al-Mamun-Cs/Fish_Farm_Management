using MediatR;
using SchoolManagement.Application.DTOs.DepositorInvestments;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.DepositorInvestments.Requests.Commands
{
    public class CreateDepositorInvestmentCommand : IRequest<BaseCommandResponse>
    {
        public CreateDepositorInvestmentDto DepositorInvestmentDto { get; set; }
    }
}
