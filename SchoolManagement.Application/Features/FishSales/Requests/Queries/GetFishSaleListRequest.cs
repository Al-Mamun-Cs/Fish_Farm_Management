using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.FishSales;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.FishSales.Requests.Queries
{
    public class GetFishSaleListRequest : IRequest<PagedResult<FishSaleDto>>
    {
        public int WarehouseId { get; set; }
        public QueryParams QueryParams { get; set; }
    }
}
