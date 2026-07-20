using MediatR;

namespace SchoolManagement.Application.Features.ShopGoodSales.Requests.Commands
{
    public class InActiveShopGoodSaleCommand : IRequest 
    {
        public int ShopGoodSaleId { get; set; }
    }
}
