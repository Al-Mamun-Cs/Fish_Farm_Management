using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.FisheriesProductTypes;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Queries
{
    public class GetFisheriesProductTypeListRequest : IRequest<PagedResult<FisheriesProductTypeDto>>
    {
        public int WarehouseId { get; set; }
        public QueryParams QueryParams { get; set; }
    }
}
