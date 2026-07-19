using SchoolManagement.Application.Features.DailyCostVaucherReasons.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.DailyCostVaucherReasons;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.DailyCostVaucherReasons.Handlers.Queries
{
    public class GetDailyCostVaucherReasonListRequestHandler : IRequestHandler<GetDailyCostVaucherReasonListRequest, PagedResult<DailyCostVaucherReasonDto>>
    {

        private readonly ISchoolManagementRepository<DailyCostVaucherReason> _DailyCostVaucherReasonRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetDailyCostVaucherReasonListRequestHandler(ISchoolManagementRepository<DailyCostVaucherReason> DailyCostVaucherReasonRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _DailyCostVaucherReasonRepository = DailyCostVaucherReasonRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<DailyCostVaucherReasonDto>> Handle(GetDailyCostVaucherReasonListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<DailyCostVaucherReason> DailyCostVaucherReasons = _DailyCostVaucherReasonRepository.FilterWithInclude(x => (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) && (x.FullName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Warehouse");
            var totalCount = DailyCostVaucherReasons.Count();
            DailyCostVaucherReasons = DailyCostVaucherReasons.OrderByDescending(x => x.DailyCostVaucherReasonId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _DailyCostVaucherReasonRepository.GetPermitedRoleFeatures(DeclareFeatureCode.DAILYCOSTVAUCHERREASON, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var DailyCostVaucherReasonDtos = _mapper.Map<List<DailyCostVaucherReasonDto>>(DailyCostVaucherReasons);
            var result = new PagedResult<DailyCostVaucherReasonDto>(DailyCostVaucherReasonDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
