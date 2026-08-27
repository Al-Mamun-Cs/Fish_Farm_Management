using MediatR;
using SchoolManagement.Application.DTOs.ProjectSchedules;

namespace SchoolManagement.Application.Features.ProjectSchedules.Requests.Queries
{
    public class GetProjectScheduleDetailRequest : IRequest<ProjectScheduleDto>
    {
        public int ProjectScheduleId { get; set; }
    }
}
