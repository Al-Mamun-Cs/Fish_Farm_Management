using MediatR;

namespace SchoolManagement.Application.Features.Warehouses.Requests.Commands
{
    public class DeleteWarehouseCommand : IRequest
    {
        public int WarehouseId { get; set; }
    }
}
