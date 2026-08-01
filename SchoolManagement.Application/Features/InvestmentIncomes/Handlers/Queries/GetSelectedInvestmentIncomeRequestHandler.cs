using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.InvestmentIncomes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.InvestmentIncomes.Handlers.Queries
{
    public class GetSelectedInvestmentIncomeRequestHandler : IRequestHandler<GetSelectedInvestmentIncomeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<InvestmentIncome> _InvestmentIncomeRepository;


        public GetSelectedInvestmentIncomeRequestHandler(ISchoolManagementRepository<InvestmentIncome> InvestmentIncomeRepository)
        {
            _InvestmentIncomeRepository = InvestmentIncomeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedInvestmentIncomeRequest request, CancellationToken cancellationToken)
        {
            ICollection<InvestmentIncome> codeValues = await _InvestmentIncomeRepository.FilterAsync(x => x.WarehouseId == request.WarehouseId);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Amount,
                Value = x.InvestmentIncomeId
            }).ToList();
            return selectModels;
        }
    }
}
