using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ShopHandCashWithdrows.Requests.Queries
{
    public class GetSelectedShopHandCashWithdrowRequest : IRequest<List<SelectedModel>>
    {
    }
}
