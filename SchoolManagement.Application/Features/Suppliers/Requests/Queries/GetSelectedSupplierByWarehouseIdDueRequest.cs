using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Suppliers.Requests.Queries
{
    public class GetSelectedSupplierByWarehouseIdDueRequest : IRequest<List<SelectedModel>>
    {
        public int WarehouseId { get; set; }
    }
}
 