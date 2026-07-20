using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ShopInventoryDetails.Requests.Queries
{
    public class GetSelectedShopInventoryProductNameRequest : IRequest<List<SelectedModel>>
    {
        public int WarehouseId { get; set; }
        public int FisheriesProductTypeId { get; set; }
    }
}
