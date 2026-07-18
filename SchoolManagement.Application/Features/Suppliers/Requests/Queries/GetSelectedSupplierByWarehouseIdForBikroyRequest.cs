using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Suppliers.Requests.Queries
{
    public class GetSelectedSupplierByWarehouseIdForBikroyRequest : IRequest<List<SelectedModel>>
    {
        public int WarehouseId { get; set; }
    }
}
