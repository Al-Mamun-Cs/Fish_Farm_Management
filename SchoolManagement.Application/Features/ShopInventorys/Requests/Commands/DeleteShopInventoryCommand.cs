using MediatR;

namespace SchoolManagement.Application.Features.ShopInventorys.Requests.Commands
{
    public class DeleteShopInventoryCommand : IRequest
    {
        public int ShopInventoryId { get; set; }
    }
}
