using SchoolManagement.Application.Features.Religions.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Religions;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.Religions.Handlers.Queries
{
    public class GetReligionListRequestHandler : IRequestHandler<GetReligionListRequest, PagedResult<ReligionDto>>
    {

        private readonly ISchoolManagementRepository<Religion> _ReligionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetReligionListRequestHandler(ISchoolManagementRepository<Religion> ReligionRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _ReligionRepository = ReligionRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<ReligionDto>> Handle(GetReligionListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Religion> Religions = _ReligionRepository.FilterWithInclude(x => (x.FullName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Religions.Count();
            Religions = Religions.OrderByDescending(x => x.ReligionId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _ReligionRepository.GetPermitedRoleFeatures(DeclareFeatureCode.RELIGION, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var ReligionDtos = _mapper.Map<List<ReligionDto>>(Religions);
            var result = new PagedResult<ReligionDto>(ReligionDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
