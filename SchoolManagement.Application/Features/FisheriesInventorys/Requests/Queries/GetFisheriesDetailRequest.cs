using MediatR;
using SchoolManagement.Application.DTOs.FisheriesInventorys;

namespace SchoolManagement.Application.Features.FisheriesInventorys.Requests.Queries
{
    public class GetFisheriesDetailRequest : IRequest<FisheriesInventoryDetailDto>
    {
        public int FisheriesInventoryDetailId { get; set; }
    }
}
