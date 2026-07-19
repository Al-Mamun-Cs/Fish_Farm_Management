using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ShopInventorys.Requests.Queries
{
    public class SpGetShopInventoryVoucherByIdRequest : IRequest<object>
    {
        public int? ShopInventoryId { get; set; }
    }
}
