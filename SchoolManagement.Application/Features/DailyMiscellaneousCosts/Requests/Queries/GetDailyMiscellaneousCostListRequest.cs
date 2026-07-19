using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.DailyMiscellaneousCosts;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Queries
{
    public class GetDailyMiscellaneousCostListRequest : IRequest<PagedResult<DailyMiscellaneousCostDto>>
    {
        public int WarehouseId { get; set; }
        public QueryParams QueryParams { get; set; }
    }
}
