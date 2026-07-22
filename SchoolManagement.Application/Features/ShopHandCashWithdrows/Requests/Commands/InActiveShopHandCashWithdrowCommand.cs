using MediatR;

namespace SchoolManagement.Application.Features.ShopHandCashWithdrows.Requests.Commands
{
    public class InActiveShopHandCashWithdrowCommand : IRequest 
    {
        public int ShopHandCashWithdrowId { get; set; }
    }
}
