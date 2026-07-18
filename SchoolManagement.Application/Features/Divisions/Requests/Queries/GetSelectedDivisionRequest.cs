using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Divisions.Requests.Queries
{
    public class GetSelectedDivisionRequest : IRequest<List<SelectedModel>>
    {
    }
}
