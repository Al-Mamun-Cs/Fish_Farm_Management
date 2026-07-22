using MediatR;
using SchoolManagement.Application.DTOs.ShopHandCashWithdrows;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ShopHandCashWithdrows.Requests.Commands
{
    public class CreateShopHandCashWithdrowCommand : IRequest<BaseCommandResponse>
    {
        public CreateShopHandCashWithdrowDto ShopHandCashWithdrowDto { get; set; }
    }
}
