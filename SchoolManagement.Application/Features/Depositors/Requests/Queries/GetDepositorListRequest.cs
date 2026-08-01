using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Depositors;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Depositors.Requests.Queries
{
    public class GetDepositorListRequest : IRequest<PagedResult<DepositorDto>>
    {
        public int WarehouseId { get; set; }
        public QueryParams QueryParams { get; set; }
    }
}
