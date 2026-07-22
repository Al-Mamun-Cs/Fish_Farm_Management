using MediatR;

namespace SchoolManagement.Application.Features.ShopHandCashWithdrows.Requests.Commands
{
    public class DeleteShopHandCashWithdrowCommand : IRequest
    {
        public int ShopHandCashWithdrowId { get; set; }
    }
}
