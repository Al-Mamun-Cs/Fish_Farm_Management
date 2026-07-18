using MediatR;
using SchoolManagement.Application.DTOs.FisheriesUnits;

namespace SchoolManagement.Application.Features.FisheriesUnits.Requests.Queries
{
    public class GetFisheriesUnitDetailRequest : IRequest<FisheriesUnitDto>
    {
        public int FisheriesUnitId { get; set; }
    }
}
