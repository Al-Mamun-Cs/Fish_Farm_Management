using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.DepositorInvestments;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.DepositorInvestments.Requests.Queries
{
    public class GetDepositorInvestmentListRequest : IRequest<PagedResult<DepositorInvestmentDto>>
    {
        public int WarehouseId { get; set; }
        public QueryParams QueryParams { get; set; }
    }
}
