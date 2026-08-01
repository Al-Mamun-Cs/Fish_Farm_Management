using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.InvestmentIncomes;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.InvestmentIncomes.Requests.Queries
{
    public class GetInvestmentIncomeListRequest : IRequest<PagedResult<InvestmentIncomeDto>>
    {
        public int WarehouseId { get; set; }
        public QueryParams QueryParams { get; set; }
    }
}
