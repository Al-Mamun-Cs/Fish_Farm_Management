using MediatR;
using SchoolManagement.Application.DTOs.ProjectTypes;

namespace SchoolManagement.Application.Features.ProjectTypes.Requests.Commands
{
    public class UpdateProjectTypeCommand : IRequest<Unit>
    {
        public ProjectTypeDto ProjectTypeDto { get; set; }
    }
}
