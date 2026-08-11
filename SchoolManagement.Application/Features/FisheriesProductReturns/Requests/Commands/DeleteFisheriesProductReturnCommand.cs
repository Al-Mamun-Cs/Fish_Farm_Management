using MediatR;

namespace SchoolManagement.Application.Features.FisheriesProductReturns.Requests.Commands
{
    public class DeleteFisheriesProductReturnCommand : IRequest
    {
        public int FisheriesProductReturnId { get; set; }
    }
}
