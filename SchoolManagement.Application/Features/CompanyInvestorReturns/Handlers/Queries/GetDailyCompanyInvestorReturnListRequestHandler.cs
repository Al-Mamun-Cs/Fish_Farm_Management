using SchoolManagement.Application.Features.CompanyInvestorReturns.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.CompanyInvestorReturns;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.CompanyInvestorReturns.Handlers.Queries
{
    public class GetCompanyInvestorReturnListRequestHandler : IRequestHandler<GetCompanyInvestorReturnListRequest, PagedResult<CompanyInvestorReturnDto>>
    {

        private readonly ISchoolManagementRepository<CompanyInvestorReturn> _CompanyInvestorReturnRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetCompanyInvestorReturnListRequestHandler(ISchoolManagementRepository<CompanyInvestorReturn> CompanyInvestorReturnRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _CompanyInvestorReturnRepository = CompanyInvestorReturnRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<CompanyInvestorReturnDto>> Handle(GetCompanyInvestorReturnListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<CompanyInvestorReturn> CompanyInvestorReturns = _CompanyInvestorReturnRepository.FilterWithInclude(x => (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) && (x.CompanyInvestor.FullName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Warehouse", "CompanyInvestor", "PaymentStatus");
            var totalCount = CompanyInvestorReturns.Count();
            CompanyInvestorReturns = CompanyInvestorReturns.OrderByDescending(x => x.CompanyInvestorReturnId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _CompanyInvestorReturnRepository.GetPermitedRoleFeatures(DeclareFeatureCode.COMPANYINVESTORRETURN, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var CompanyInvestorReturnDtos = _mapper.Map<List<CompanyInvestorReturnDto>>(CompanyInvestorReturns);
            var result = new PagedResult<CompanyInvestorReturnDto>(CompanyInvestorReturnDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
