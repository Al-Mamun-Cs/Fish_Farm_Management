using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Warehouses.Requests.Queries
{
    public class GetSelectedWarehouseRequest : IRequest<List<SelectedModel>>
    {
    }
}
