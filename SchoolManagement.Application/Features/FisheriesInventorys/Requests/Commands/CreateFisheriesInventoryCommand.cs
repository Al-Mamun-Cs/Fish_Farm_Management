using MediatR;
using SchoolManagement.Application.DTOs.FisheriesInventorys;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.FisheriesInventorys.Requests.Commands
{
    public class CreateFisheriesInventoryCommand : IRequest<BaseCommandResponse>
    {
        public CreateFisheriesInventoryDetailDto FisheriesInventoryDetailDto { get; set; }
    }
}
