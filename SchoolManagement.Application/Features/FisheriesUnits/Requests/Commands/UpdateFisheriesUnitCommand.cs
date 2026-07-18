using MediatR;
using SchoolManagement.Application.DTOs.FisheriesUnits;

namespace SchoolManagement.Application.Features.FisheriesUnits.Requests.Commands
{
    public class UpdateFisheriesUnitCommand : IRequest<Unit>
    {
        public FisheriesUnitDto FisheriesUnitDto { get; set; }
    }
}
