using MediatR;
using SchoolManagement.Application.DTOs.FisheriesInventoryOuts;

namespace SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Queries
{
    public class GetFisheriesInventoryOutDetailRequest : IRequest<FisheriesInventoryOutDto>
    {
        public int FisheriesInventoryOutId { get; set; }
    }
}
