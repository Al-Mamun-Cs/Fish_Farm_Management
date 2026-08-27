using MediatR;

namespace SchoolManagement.Application.Features.ProjectSchedules.Requests.Commands
{
    public class DeleteProjectScheduleCommand : IRequest
    {
        public int ProjectScheduleId { get; set; }
    }
}
