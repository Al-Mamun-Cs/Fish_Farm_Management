using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Warehouses.Requests.Queries
{
    public class GetSelectedWarehouseByIdRequest : IRequest<List<SelectedModel>>
    {
        public int WarehouseId { get; set; }
    }
}
