using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Warehouses;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Warehouses.Requests.Queries
{
    public class GetWarehouseListRequest : IRequest<PagedResult<WarehouseDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
