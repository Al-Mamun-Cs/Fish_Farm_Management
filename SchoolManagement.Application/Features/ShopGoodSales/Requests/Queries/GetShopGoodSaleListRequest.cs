using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ShopGoodSales;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ShopGoodSales.Requests.Queries
{
    public class GetShopGoodSaleListRequest : IRequest<PagedResult<ShopGoodSaleDto>>
    {
        public int WarehouseId { get; set; }
        public QueryParams QueryParams { get; set; }
    }
}
