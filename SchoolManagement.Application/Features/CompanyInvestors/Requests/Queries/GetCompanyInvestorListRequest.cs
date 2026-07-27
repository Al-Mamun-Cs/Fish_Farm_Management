using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.CompanyInvestors;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.CompanyInvestors.Requests.Queries
{
    public class GetCompanyInvestorListRequest : IRequest<PagedResult<CompanyInvestorDto>>
    {
        public int WarehouseId { get; set; }
        public QueryParams QueryParams { get; set; }
    }
}
