using SchoolManagement.Application.Features.FisheriesUnits.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.FisheriesUnits;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.FisheriesUnits.Handlers.Queries
{
    public class GetFisheriesUnitListRequestHandler : IRequestHandler<GetFisheriesUnitListRequest, PagedResult<FisheriesUnitDto>>
    {

        private readonly ISchoolManagementRepository<FisheriesUnit> _FisheriesUnitRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetFisheriesUnitListRequestHandler(ISchoolManagementRepository<FisheriesUnit> FisheriesUnitRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _FisheriesUnitRepository = FisheriesUnitRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<FisheriesUnitDto>> Handle(GetFisheriesUnitListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<FisheriesUnit> FisheriesUnits = _FisheriesUnitRepository.FilterWithInclude(x => (x.FullName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = FisheriesUnits.Count();
            FisheriesUnits = FisheriesUnits.OrderByDescending(x => x.FisheriesUnitId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _FisheriesUnitRepository.GetPermitedRoleFeatures(DeclareFeatureCode.FISHERIESUNIT, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var FisheriesUnitDtos = _mapper.Map<List<FisheriesUnitDto>>(FisheriesUnits);
            var result = new PagedResult<FisheriesUnitDto>(FisheriesUnitDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
