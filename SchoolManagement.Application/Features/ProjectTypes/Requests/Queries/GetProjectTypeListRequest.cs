using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ProjectTypes;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ProjectTypes.Requests.Queries
{
    public class GetProjectTypeListRequest : IRequest<PagedResult<ProjectTypeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
