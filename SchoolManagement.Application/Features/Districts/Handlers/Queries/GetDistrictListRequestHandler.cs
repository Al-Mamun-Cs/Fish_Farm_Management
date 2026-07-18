using SchoolManagement.Application.Features.Districts.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Districts;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.Districts.Handlers.Queries
{
    public class GetUpozilaListRequestHandler : IRequestHandler<GetDistrictListRequest, PagedResult<DistrictDto>>
    {

        private readonly ISchoolManagementRepository<District> _DistrictRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetUpozilaListRequestHandler(ISchoolManagementRepository<District> DistrictRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _DistrictRepository = DistrictRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<DistrictDto>> Handle(GetDistrictListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<District> Districts = _DistrictRepository.FilterWithInclude(x => (x.DistrictName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Division");
            var totalCount = Districts.Count();
            Districts = Districts.OrderByDescending(x => x.DistrictId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _DistrictRepository.GetPermitedRoleFeatures(DeclareFeatureCode.DISTRICT, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var DistrictDtos = _mapper.Map<List<DistrictDto>>(Districts);
            var result = new PagedResult<DistrictDto>(DistrictDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
