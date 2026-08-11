using MediatR;
using SchoolManagement.Application.DTOs.FisheriesProductReturns;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.FisheriesProductReturns.Requests.Commands
{
    public class CreateFisheriesProductReturnCommand : IRequest<BaseCommandResponse>
    {
        public CreateFisheriesProductReturnDto FisheriesProductReturnDto { get; set; }
    }
}
