using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Fiscalyears.Requests.Queries
{
    public class GetSelectedFiscalyearRequest : IRequest<List<SelectedModel>>
    {
    }
}
