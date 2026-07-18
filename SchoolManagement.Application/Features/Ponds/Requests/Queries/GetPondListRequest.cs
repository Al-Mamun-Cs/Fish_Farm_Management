using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Ponds;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Ponds.Requests.Queries
{
    public class GetPondListRequest : IRequest<PagedResult<PondDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
