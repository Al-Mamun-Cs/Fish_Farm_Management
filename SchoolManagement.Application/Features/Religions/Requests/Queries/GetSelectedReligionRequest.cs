using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Religions.Requests.Queries
{
    public class GetSelectedReligionRequest : IRequest<List<SelectedModel>>
    {
    }
}
