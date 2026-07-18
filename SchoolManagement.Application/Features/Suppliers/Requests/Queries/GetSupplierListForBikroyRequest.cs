using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Suppliers;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Suppliers.Requests.Queries
{
    public class GetSupplierListForBikroyRequest : IRequest<PagedResult<SupplierDto>>
    {
        public int WarehouseId { get; set; }
        public QueryParams QueryParams { get; set; }
    }
}
