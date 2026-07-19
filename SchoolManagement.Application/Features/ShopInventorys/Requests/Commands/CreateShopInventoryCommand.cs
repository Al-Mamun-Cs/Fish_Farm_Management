using MediatR;
using SchoolManagement.Application.DTOs.ShopInventorys;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ShopInventorys.Requests.Commands
{
    public class CreateShopInventoryCommand : IRequest<BaseCommandResponse>
    {
        public CreateShopInventoryDetailDto ShopInventoryDetailDto { get; set; }
    }
}
