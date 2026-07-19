using MediatR;
using SchoolManagement.Application.DTOs.ShopInventorys;

namespace SchoolManagement.Application.Features.ShopInventorys.Requests.Queries
{
    public class GetShopInventoryDetailRequest : IRequest<ShopInventoryDto>
    {
        public int ShopInventoryId { get; set; }
    }
}
