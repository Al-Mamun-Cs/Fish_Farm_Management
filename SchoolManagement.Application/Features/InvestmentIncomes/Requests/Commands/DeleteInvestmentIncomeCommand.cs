using MediatR;

namespace SchoolManagement.Application.Features.InvestmentIncomes.Requests.Commands
{
    public class DeleteInvestmentIncomeCommand : IRequest
    {
        public int InvestmentIncomeId { get; set; }
    }
}
