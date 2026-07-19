using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.DailyCostVaucherReasons;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.DailyCostVaucherReasons.Requests.Queries
{
    public class GetDailyCostVaucherReasonListRequest : IRequest<PagedResult<DailyCostVaucherReasonDto>>
    {
        public int WarehouseId { get; set; }
        public QueryParams QueryParams { get; set; }
    }
}
