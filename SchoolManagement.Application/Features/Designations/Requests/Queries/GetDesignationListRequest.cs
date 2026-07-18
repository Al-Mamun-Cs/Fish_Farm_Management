using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Designations;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Designations.Requests.Queries
{
    public class GetDesignationListRequest : IRequest<PagedResult<DesignationDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
