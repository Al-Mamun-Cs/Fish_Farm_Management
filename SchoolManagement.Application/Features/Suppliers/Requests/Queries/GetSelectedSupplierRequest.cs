using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Suppliers.Requests.Queries
{
    public class GetSelectedSupplierRequest : IRequest<List<SelectedModel>>
    {
        public int WarehouseId { get; set; }
        public int SupplierStatus { get; set; }
    }
}
