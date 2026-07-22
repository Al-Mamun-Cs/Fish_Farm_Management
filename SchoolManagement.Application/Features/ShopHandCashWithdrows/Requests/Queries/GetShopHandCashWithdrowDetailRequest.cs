using MediatR;
using SchoolManagement.Application.DTOs.ShopHandCashWithdrows;

namespace SchoolManagement.Application.Features.ShopHandCashWithdrows.Requests.Queries
{
    public class GetShopHandCashWithdrowDetailRequest : IRequest<ShopHandCashWithdrowDto>
    {
        public int ShopHandCashWithdrowId { get; set; }
    }
}
