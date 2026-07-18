using MediatR;
using SchoolManagement.Application.DTOs.FisheriesInventorys;

namespace SchoolManagement.Application.Features.FisheriesInventorys.Requests.Queries
{
    public class GetFisheriesInventoryDetailRequest : IRequest<FisheriesInventoryDto>
    {
        public int FisheriesInventoryId { get; set; }
    }
}
