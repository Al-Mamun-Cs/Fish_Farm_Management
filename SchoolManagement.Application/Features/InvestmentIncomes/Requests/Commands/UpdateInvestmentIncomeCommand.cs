using MediatR;
using SchoolManagement.Application.DTOs.InvestmentIncomes;

namespace SchoolManagement.Application.Features.InvestmentIncomes.Requests.Commands
{
    public class UpdateInvestmentIncomeCommand : IRequest<Unit>
    {
        public InvestmentIncomeDto InvestmentIncomeDto { get; set; }
    }
}
