using MediatR;
using SchoolManagement.Application.DTOs.InvestmentIncomes;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.InvestmentIncomes.Requests.Commands
{
    public class CreateInvestmentIncomeCommand : IRequest<BaseCommandResponse>
    {
        public CreateInvestmentIncomeDto InvestmentIncomeDto { get; set; }
    }
}
