using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.BloodGroups;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.BloodGroups.Requests.Queries
{
    public class GetBloodGroupListRequest : IRequest<PagedResult<BloodGroupDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
