using SchoolManagement.Application.Features.Fiscalyears.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Fiscalyears;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.Fiscalyears.Handlers.Queries
{
    public class GetFiscalyearListRequestHandler : IRequestHandler<GetFiscalyearListRequest, PagedResult<FiscalyearDto>>
    {

        private readonly ISchoolManagementRepository<Fiscalyear> _FiscalyearRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetFiscalyearListRequestHandler(ISchoolManagementRepository<Fiscalyear> FiscalyearRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _FiscalyearRepository = FiscalyearRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<FiscalyearDto>> Handle(GetFiscalyearListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Fiscalyear> Fiscalyears = _FiscalyearRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Fiscalyears.Count();
            Fiscalyears = Fiscalyears.OrderByDescending(x => x.FiscalyearId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _FiscalyearRepository.GetPermitedRoleFeatures(DeclareFeatureCode.FISCALYEAR, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var FiscalyearDtos = _mapper.Map<List<FiscalyearDto>>(Fiscalyears);
            var result = new PagedResult<FiscalyearDto>(FiscalyearDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
