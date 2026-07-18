using MediatR;
using SchoolManagement.Application.DTOs.FisheriesInventoryOuts;

namespace SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Commands
{
    public class UpdateFisheriesInventoryOutCommand : IRequest<Unit>
    {
        public FisheriesInventoryOutDto FisheriesInventoryOutDto { get; set; }
    }
}
