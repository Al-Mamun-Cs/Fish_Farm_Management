using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ShopGoodSales.Requests.Queries
{
    public class SpGetShopGoodSaleVoucherByIdRequest : IRequest<object>
    {
        public int? ShopGoodSaleId { get; set; }
    }
}
