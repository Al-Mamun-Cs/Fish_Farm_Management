using MediatR;

namespace SchoolManagement.Application.Features.ShopGoodSales.Requests.Commands
{
    public class DeleteShopGoodSaleCommand : IRequest
    {
        public int ShopGoodSaleId { get; set; }
    }
}
