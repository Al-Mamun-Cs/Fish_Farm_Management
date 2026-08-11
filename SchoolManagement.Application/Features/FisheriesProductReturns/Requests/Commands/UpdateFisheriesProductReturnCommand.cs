using MediatR;
using SchoolManagement.Application.DTOs.FisheriesProductReturns;

namespace SchoolManagement.Application.Features.FisheriesProductReturns.Requests.Commands
{
    public class UpdateFisheriesProductReturnCommand : IRequest<Unit>
    {
        public FisheriesProductReturnDto FisheriesProductReturnDto { get; set; }
    }
}
