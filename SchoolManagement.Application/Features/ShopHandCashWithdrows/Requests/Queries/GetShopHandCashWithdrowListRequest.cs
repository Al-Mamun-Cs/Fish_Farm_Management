using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ShopHandCashWithdrows;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ShopHandCashWithdrows.Requests.Queries
{
    public class GetShopHandCashWithdrowListRequest : IRequest<PagedResult<ShopHandCashWithdrowDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
