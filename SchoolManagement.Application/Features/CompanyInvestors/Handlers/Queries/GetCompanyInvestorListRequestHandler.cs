using SchoolManagement.Application.Features.CompanyInvestors.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.CompanyInvestors;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.CompanyInvestors.Handlers.Queries
{
    public class GetCompanyInvestorListRequestHandler : IRequestHandler<GetCompanyInvestorListRequest, PagedResult<CompanyInvestorDto>>
    {

        private readonly ISchoolManagementRepository<CompanyInvestor> _CompanyInvestorRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetCompanyInvestorListRequestHandler(ISchoolManagementRepository<CompanyInvestor> CompanyInvestorRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _CompanyInvestorRepository = CompanyInvestorRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<CompanyInvestorDto>> Handle(GetCompanyInvestorListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<CompanyInvestor> CompanyInvestors = _CompanyInvestorRepository.FilterWithInclude(x => (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) && (x.FullName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Warehouse");
            var totalCount = CompanyInvestors.Count();
            CompanyInvestors = CompanyInvestors.OrderByDescending(x => x.CompanyInvestorId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _CompanyInvestorRepository.GetPermitedRoleFeatures(DeclareFeatureCode.COMPANYINVESTOR, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var CompanyInvestorDtos = _mapper.Map<List<CompanyInvestorDto>>(CompanyInvestors);
            var result = new PagedResult<CompanyInvestorDto>(CompanyInvestorDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
