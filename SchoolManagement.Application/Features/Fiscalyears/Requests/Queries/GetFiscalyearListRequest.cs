using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Fiscalyears;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Fiscalyears.Requests.Queries
{
    public class GetFiscalyearListRequest : IRequest<PagedResult<FiscalyearDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
