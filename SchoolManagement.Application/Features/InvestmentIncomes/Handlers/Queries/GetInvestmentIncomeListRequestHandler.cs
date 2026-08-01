using SchoolManagement.Application.Features.InvestmentIncomes.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.InvestmentIncomes;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.InvestmentIncomes.Handlers.Queries
{
    public class GetInvestmentIncomeListRequestHandler : IRequestHandler<GetInvestmentIncomeListRequest, PagedResult<InvestmentIncomeDto>>
    {

        private readonly ISchoolManagementRepository<InvestmentIncome> _InvestmentIncomeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetInvestmentIncomeListRequestHandler(ISchoolManagementRepository<InvestmentIncome> InvestmentIncomeRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _InvestmentIncomeRepository = InvestmentIncomeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<InvestmentIncomeDto>> Handle(GetInvestmentIncomeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<InvestmentIncome> InvestmentIncomes = _InvestmentIncomeRepository.FilterWithInclude(x => (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) && (x.DepositorInvestment.BusinessOperatorName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Warehouse", "DepositorInvestment");
            var totalCount = InvestmentIncomes.Count();
            InvestmentIncomes = InvestmentIncomes.OrderByDescending(x => x.InvestmentIncomeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _InvestmentIncomeRepository.GetPermitedRoleFeatures(DeclareFeatureCode.INVESTMENTINCOME, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var InvestmentIncomeDtos = _mapper.Map<List<InvestmentIncomeDto>>(InvestmentIncomes);
            var result = new PagedResult<InvestmentIncomeDto>(InvestmentIncomeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
