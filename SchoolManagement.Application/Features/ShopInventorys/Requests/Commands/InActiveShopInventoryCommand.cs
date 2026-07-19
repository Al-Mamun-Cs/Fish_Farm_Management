using MediatR;

namespace SchoolManagement.Application.Features.ShopInventorys.Requests.Commands
{
    public class InActiveShopInventoryCommand : IRequest 
    {
        public int ShopInventoryId { get; set; }
    }
}
