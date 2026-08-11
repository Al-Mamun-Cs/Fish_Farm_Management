using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.FisheriesProductReturns;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.FisheriesProductReturns.Requests.Queries
{
    public class GetFisheriesProductReturnListRequest : IRequest<PagedResult<FisheriesProductReturnDto>>
    {
        public int WarehouseId { get; set; }
        public QueryParams QueryParams { get; set; }
    }
}
