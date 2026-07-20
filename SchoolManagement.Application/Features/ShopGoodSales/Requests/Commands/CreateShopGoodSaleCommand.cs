using MediatR;
using SchoolManagement.Application.DTOs.ShopGoodSales;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ShopGoodSales.Requests.Commands
{
    public class CreateShopGoodSaleCommand : IRequest<BaseCommandResponse>
    {
        public CreateShopGoodSaleDetailDto ShopGoodSaleDetailDto { get; set; }
    }
}
