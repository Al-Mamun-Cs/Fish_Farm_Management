using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.SupplierTypes.Requests.Queries
{
    public class GetSelectedSupplierTypeRequest : IRequest<List<SelectedModel>>
    {
    }
}
