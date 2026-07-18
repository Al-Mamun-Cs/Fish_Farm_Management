using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.FisheriesInventorys;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.FisheriesInventorys.Requests.Queries
{
    public class GetFisheriesInventoryListRequest : IRequest<PagedResult<FisheriesInventoryDto>>
    {
        public int WarehouseId { get; set; }
        public QueryParams QueryParams { get; set; }
    }
}
