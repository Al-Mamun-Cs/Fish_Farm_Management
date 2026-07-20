using MediatR;
using SchoolManagement.Application.DTOs.ShopGoodSales;

namespace SchoolManagement.Application.Features.ShopGoodSales.Requests.Queries
{
    public class GetShopGoodSaleDetailRequest : IRequest<ShopGoodSaleDto>
    {
        public int ShopGoodSaleId { get; set; }
    }
}
