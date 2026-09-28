using MediatR;
using SchoolManagement.Application.DTOs.ProjectTypes;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ProjectTypes.Requests.Commands
{
    public class CreateProjectTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateProjectTypeDto ProjectTypeDto { get; set; }
    }
}
