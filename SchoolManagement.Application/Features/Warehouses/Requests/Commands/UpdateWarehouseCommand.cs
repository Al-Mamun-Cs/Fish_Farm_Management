using MediatR;
using SchoolManagement.Application.DTOs.Warehouses;

namespace SchoolManagement.Application.Features.Warehouses.Requests.Commands
{
    public class UpdateWarehouseCommand : IRequest<Unit>
    {
        public CreateWarehouseDto CreateWarehouseDto { get; set; }
    }
}
