using MediatR;

namespace SchoolManagement.Application.Features.FisheriesProductReturns.Requests.Commands
{
    public class InActiveFisheriesProductReturnCommand : IRequest 
    {
        public int FisheriesProductReturnId { get; set; }
    }
}
