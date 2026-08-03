using MediatR;

namespace SchoolManagement.Application.Features.DepositorInvestments.Requests.Commands
{
    public class CloseDepositorInvestmentCommand : IRequest 
    {
        public int DepositorInvestmentId { get; set; }
    }
}
