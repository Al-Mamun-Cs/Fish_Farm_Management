using MediatR;
using SchoolManagement.Application.DTOs.ProjectSchedules;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ProjectSchedules.Requests.Commands
{
    public class CreateProjectScheduleCommand : IRequest<BaseCommandResponse>
    {
        public CreateProjectScheduleDto ProjectScheduleDto { get; set; }
    }
}
