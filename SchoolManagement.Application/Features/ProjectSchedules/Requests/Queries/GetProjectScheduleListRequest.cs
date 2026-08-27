using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ProjectSchedules;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ProjectSchedules.Requests.Queries
{
    public class GetProjectScheduleListRequest : IRequest<PagedResult<ProjectScheduleDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
