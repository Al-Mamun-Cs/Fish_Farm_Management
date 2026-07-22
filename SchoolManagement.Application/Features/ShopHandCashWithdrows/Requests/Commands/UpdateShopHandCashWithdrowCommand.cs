using MediatR;
using SchoolManagement.Application.DTOs.ShopHandCashWithdrows;

namespace SchoolManagement.Application.Features.ShopHandCashWithdrows.Requests.Commands
{
    public class UpdateShopHandCashWithdrowCommand : IRequest<Unit>
    {
        public ShopHandCashWithdrowDto ShopHandCashWithdrowDto { get; set; }
    }
}
