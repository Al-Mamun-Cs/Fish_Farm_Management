using MediatR;
using SchoolManagement.Application.DTOs.InvestmentIncomes;

namespace SchoolManagement.Application.Features.InvestmentIncomes.Requests.Queries
{
    public class GetInvestmentIncomeDetailRequest : IRequest<InvestmentIncomeDto>
    {
        public int InvestmentIncomeId { get; set; }
    }
}
