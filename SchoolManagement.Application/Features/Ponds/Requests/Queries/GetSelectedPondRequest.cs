using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Ponds.Requests.Queries
{
    public class GetSelectedPondRequest : IRequest<List<SelectedModel>>
    {
    }
}
