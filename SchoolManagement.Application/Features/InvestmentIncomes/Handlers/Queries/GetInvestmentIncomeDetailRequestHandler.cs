using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.InvestmentIncomes;
using SchoolManagement.Application.Features.InvestmentIncomes.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.InvestmentIncomes.Handlers.Queries
{
    public class GetInvestmentIncomeDetailRequestHandler : IRequestHandler<GetInvestmentIncomeDetailRequest, InvestmentIncomeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<InvestmentIncome> _InvestmentIncomeRepository;
        public GetInvestmentIncomeDetailRequestHandler(ISchoolManagementRepository<InvestmentIncome> InvestmentIncomeRepository, IMapper mapper)
        {
            _InvestmentIncomeRepository = InvestmentIncomeRepository;
            _mapper = mapper;
        }
        public async Task<InvestmentIncomeDto> Handle(GetInvestmentIncomeDetailRequest request, CancellationToken cancellationToken)
        {
            var InvestmentIncome = await _InvestmentIncomeRepository.Get(request.InvestmentIncomeId);
            return _mapper.Map<InvestmentIncomeDto>(InvestmentIncome);
        }
    }
}
