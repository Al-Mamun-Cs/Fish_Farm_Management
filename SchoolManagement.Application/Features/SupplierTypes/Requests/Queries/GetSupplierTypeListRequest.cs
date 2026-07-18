using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.SupplierTypes;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.SupplierTypes.Requests.Queries
{
    public class GetSupplierTypeListRequest : IRequest<PagedResult<SupplierTypeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
