using SchoolManagement.Application.Features.Genders.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Genders;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.Genders.Handlers.Queries
{
    public class GetGenderListRequestHandler : IRequestHandler<GetGenderListRequest, PagedResult<GenderDto>>
    {

        private readonly ISchoolManagementRepository<Gender> _GenderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetGenderListRequestHandler(ISchoolManagementRepository<Gender> GenderRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _GenderRepository = GenderRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<GenderDto>> Handle(GetGenderListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Gender> Genders = _GenderRepository.FilterWithInclude(x => (x.FullName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Genders.Count();
            Genders = Genders.OrderByDescending(x => x.GenderId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _GenderRepository.GetPermitedRoleFeatures(DeclareFeatureCode.GENDER, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var GenderDtos = _mapper.Map<List<GenderDto>>(Genders);
            var result = new PagedResult<GenderDto>(GenderDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
