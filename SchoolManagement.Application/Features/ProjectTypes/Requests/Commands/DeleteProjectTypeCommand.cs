using MediatR;

namespace SchoolManagement.Application.Features.ProjectTypes.Requests.Commands
{
    public class DeleteProjectTypeCommand : IRequest
    {
        public int ProjectTypeId { get; set; }
    }
}
