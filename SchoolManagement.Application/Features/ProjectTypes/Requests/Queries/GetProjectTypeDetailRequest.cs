using MediatR;
using SchoolManagement.Application.DTOs.ProjectTypes;

namespace SchoolManagement.Application.Features.ProjectTypes.Requests.Queries
{
    public class GetProjectTypeDetailRequest : IRequest<ProjectTypeDto>
    {
        public int ProjectTypeId { get; set; }
    }
}
