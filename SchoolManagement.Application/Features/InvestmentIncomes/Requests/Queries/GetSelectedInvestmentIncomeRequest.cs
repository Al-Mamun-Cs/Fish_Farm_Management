using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.InvestmentIncomes.Requests.Queries
{
    public class GetSelectedInvestmentIncomeRequest : IRequest<List<SelectedModel>>
    {
        public int WarehouseId { get; set; }
    }
}
