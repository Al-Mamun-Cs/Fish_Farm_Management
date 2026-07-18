using MediatR;
using SchoolManagement.Application.DTOs.Warehouses;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Warehouses.Requests.Commands
{
    public class CreateWarehouseCommand : IRequest<BaseCommandResponse>
    {
        public CreateWarehouseDto WarehouseDto { get; set; }
    }
}
