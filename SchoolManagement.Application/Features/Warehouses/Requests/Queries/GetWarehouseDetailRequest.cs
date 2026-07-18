using MediatR;
using SchoolManagement.Application.DTOs.Warehouses;

namespace SchoolManagement.Application.Features.Warehouses.Requests.Queries
{
    public class GetWarehouseDetailRequest : IRequest<WarehouseDto>
    {
        public int WarehouseId { get; set; }
    }
}
