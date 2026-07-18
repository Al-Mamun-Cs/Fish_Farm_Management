using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.FisheriesUnits;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.FisheriesUnits.Requests.Queries
{
    public class GetFisheriesUnitListRequest : IRequest<PagedResult<FisheriesUnitDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
