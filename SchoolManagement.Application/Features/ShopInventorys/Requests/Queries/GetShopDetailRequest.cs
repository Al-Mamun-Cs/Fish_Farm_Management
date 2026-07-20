using MediatR;
using SchoolManagement.Application.DTOs.ShopInventorys;

namespace SchoolManagement.Application.Features.ShopInventorys.Requests.Queries
{
    public class GetShopDetailRequest : IRequest<ShopInventoryDetailDto>
    {
        public int ShopInventoryDetailId { get; set; }
    }
}
