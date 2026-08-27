using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ProjectSchedules.Requests.Queries
{
    public class GetSelectedProjectScheduleRequest : IRequest<List<SelectedModel>>
    {
    }
}
