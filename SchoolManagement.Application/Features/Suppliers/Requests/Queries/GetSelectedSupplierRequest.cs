using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Suppliers.Requests.Queries
{
    public class GetSelectedSupplierRequest : IRequest<List<SelectedModel>>
    {
    }
}
