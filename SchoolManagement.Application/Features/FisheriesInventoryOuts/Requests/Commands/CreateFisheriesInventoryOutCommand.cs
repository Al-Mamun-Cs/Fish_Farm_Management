using MediatR;
using SchoolManagement.Application.DTOs.FisheriesInventoryOuts;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Commands
{
    public class CreateFisheriesInventoryOutCommand : IRequest<BaseCommandResponse>
    {
        public CreateFisheriesInventoryOutDto FisheriesInventoryOutDto { get; set; }
    }
}
