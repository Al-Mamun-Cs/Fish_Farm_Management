using MediatR;

namespace SchoolManagement.Application.Features.FisheriesUnits.Requests.Commands
{
    public class DeleteFisheriesUnitCommand : IRequest
    {
        public int FisheriesUnitId { get; set; }
    }
}
