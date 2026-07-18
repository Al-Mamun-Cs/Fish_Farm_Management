using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Divisions;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Divisions.Requests.Queries
{
    public class GetDivisionListRequest : IRequest<PagedResult<DivisionDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
