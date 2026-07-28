using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.CompanyInvestorReturns;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.CompanyInvestorReturns.Requests.Queries
{
    public class GetCompanyInvestorReturnListRequest : IRequest<PagedResult<CompanyInvestorReturnDto>>
    {
        public int WarehouseId { get; set; }
        public QueryParams QueryParams { get; set; }
    }
}
