using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ProjectTypes.Requests.Queries
{
    public class GetSelectedProjectTypeRequest : IRequest<List<SelectedModel>>
    {
    }
}
