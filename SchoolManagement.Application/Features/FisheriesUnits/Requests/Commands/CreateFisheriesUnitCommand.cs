using MediatR;
using SchoolManagement.Application.DTOs.FisheriesUnits;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.FisheriesUnits.Requests.Commands
{
    public class CreateFisheriesUnitCommand : IRequest<BaseCommandResponse>
    {
        public CreateFisheriesUnitDto FisheriesUnitDto { get; set; }
    }
}
