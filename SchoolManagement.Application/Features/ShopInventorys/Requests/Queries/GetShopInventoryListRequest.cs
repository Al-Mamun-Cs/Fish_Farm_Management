using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ShopInventorys;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ShopInventorys.Requests.Queries
{
    public class GetShopInventoryListRequest : IRequest<PagedResult<ShopInventoryDto>>
    {
        public int WarehouseId { get; set; }
        public QueryParams QueryParams { get; set; }
    }
}
