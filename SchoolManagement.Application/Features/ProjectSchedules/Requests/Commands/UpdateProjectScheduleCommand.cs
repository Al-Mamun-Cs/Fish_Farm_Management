using MediatR;
using SchoolManagement.Application.DTOs.ProjectSchedules;

namespace SchoolManagement.Application.Features.ProjectSchedules.Requests.Commands
{
    public class UpdateProjectScheduleCommand : IRequest<Unit>
    {
        public ProjectScheduleDto ProjectScheduleDto { get; set; }
    }
}
