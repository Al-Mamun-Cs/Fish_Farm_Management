using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.BloodGroups.Requests.Queries
{
    public class GetSelectedBloodGroupRequest : IRequest<List<SelectedModel>>
    {
    }
}
