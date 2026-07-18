using SchoolManagement.Application.Features.Countrys.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Countrys;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.Countrys.Handlers.Queries
{
    public class GetCountryListRequestHandler : IRequestHandler<GetCountryListRequest, PagedResult<CountryDto>>
    {

        private readonly ISchoolManagementRepository<Country> _CountryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetCountryListRequestHandler(ISchoolManagementRepository<Country> CountryRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _CountryRepository = CountryRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<CountryDto>> Handle(GetCountryListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Country> Countrys = _CountryRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Countrys.Count();
            Countrys = Countrys.OrderByDescending(x => x.CountryId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _CountryRepository.GetPermitedRoleFeatures(DeclareFeatureCode.COUNTRY, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var CountryDtos = _mapper.Map<List<CountryDto>>(Countrys);
            var result = new PagedResult<CountryDto>(CountryDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
