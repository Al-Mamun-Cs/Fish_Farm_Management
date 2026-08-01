using MediatR;

namespace SchoolManagement.Application.Features.DepositorInvestments.Requests.Commands
{
    public class InActiveDepositorInvestmentCommand : IRequest 
    {
        public int DepositorInvestmentId { get; set; }
    }
}
