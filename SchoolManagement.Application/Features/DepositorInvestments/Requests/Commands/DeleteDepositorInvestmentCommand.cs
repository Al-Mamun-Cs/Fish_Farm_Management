using MediatR;

namespace SchoolManagement.Application.Features.DepositorInvestments.Requests.Commands
{
    public class DeleteDepositorInvestmentCommand : IRequest
    {
        public int DepositorInvestmentId { get; set; }
    }
}
