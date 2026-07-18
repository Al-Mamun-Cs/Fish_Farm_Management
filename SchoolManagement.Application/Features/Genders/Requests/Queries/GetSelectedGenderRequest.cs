using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Genders.Requests.Queries
{
    public class GetSelectedGenderRequest : IRequest<List<SelectedModel>>
    {
    }
}
