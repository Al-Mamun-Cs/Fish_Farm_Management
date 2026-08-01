using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.DepositorInstallments;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.DepositorInstallments.Requests.Queries
{
    public class GetDepositorInstallmentListRequest : IRequest<PagedResult<DepositorInstallmentDto>>
    {
        public int WarehouseId { get; set; }
        public QueryParams QueryParams { get; set; }
    }
}
