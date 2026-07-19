using SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.DailyMiscellaneousCosts;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Handlers.Queries
{
    public class GetDailyMiscellaneousCostListRequestHandler : IRequestHandler<GetDailyMiscellaneousCostListRequest, PagedResult<DailyMiscellaneousCostDto>>
    {

        private readonly ISchoolManagementRepository<DailyMiscellaneousCost> _DailyMiscellaneousCostRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetDailyMiscellaneousCostListRequestHandler(ISchoolManagementRepository<DailyMiscellaneousCost> DailyMiscellaneousCostRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _DailyMiscellaneousCostRepository = DailyMiscellaneousCostRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<DailyMiscellaneousCostDto>> Handle(GetDailyMiscellaneousCostListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<DailyMiscellaneousCost> DailyMiscellaneousCosts = _DailyMiscellaneousCostRepository.FilterWithInclude(x => (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) && (x.DailyCostVaucherReason.FullName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Warehouse", "DailyCostVaucherReason", "PaymentStatus");
            var totalCount = DailyMiscellaneousCosts.Count();
            DailyMiscellaneousCosts = DailyMiscellaneousCosts.OrderByDescending(x => x.DailyMiscellaneousCostId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _DailyMiscellaneousCostRepository.GetPermitedRoleFeatures(DeclareFeatureCode.DAILYMISCELLANEOUSCOST, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var DailyMiscellaneousCostDtos = _mapper.Map<List<DailyMiscellaneousCostDto>>(DailyMiscellaneousCosts);
            var result = new PagedResult<DailyMiscellaneousCostDto>(DailyMiscellaneousCostDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
