using SchoolManagement.Application.Features.BloodGroups.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.BloodGroups;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.BloodGroups.Handlers.Queries
{
    public class GetBloodGroupListRequestHandler : IRequestHandler<GetBloodGroupListRequest, PagedResult<BloodGroupDto>>
    {

        private readonly ISchoolManagementRepository<BloodGroup> _BloodGroupRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetBloodGroupListRequestHandler(ISchoolManagementRepository<BloodGroup> BloodGroupRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _BloodGroupRepository = BloodGroupRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<BloodGroupDto>> Handle(GetBloodGroupListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<BloodGroup> BloodGroups = _BloodGroupRepository.FilterWithInclude(x => (x.FullName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = BloodGroups.Count();
            BloodGroups = BloodGroups.OrderByDescending(x => x.BloodGroupId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _BloodGroupRepository.GetPermitedRoleFeatures(DeclareFeatureCode.BLOODGROUP, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var BloodGroupDtos = _mapper.Map<List<BloodGroupDto>>(BloodGroups);
            var result = new PagedResult<BloodGroupDto>(BloodGroupDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
