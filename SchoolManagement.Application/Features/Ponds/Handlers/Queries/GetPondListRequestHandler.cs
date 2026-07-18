using SchoolManagement.Application.Features.Ponds.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Ponds;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.Ponds.Handlers.Queries
{
    public class GetPondListRequestHandler : IRequestHandler<GetPondListRequest, PagedResult<PondDto>>
    {

        private readonly ISchoolManagementRepository<Pond> _PondRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetPondListRequestHandler(ISchoolManagementRepository<Pond> PondRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _PondRepository = PondRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<PondDto>> Handle(GetPondListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Pond> Ponds = _PondRepository.FilterWithInclude(x => (x.NameEnglish.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Ponds.Count();
            Ponds = Ponds.OrderByDescending(x => x.PondId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _PondRepository.GetPermitedRoleFeatures(DeclareFeatureCode.POND, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var PondDtos = _mapper.Map<List<PondDto>>(Ponds);
            var result = new PagedResult<PondDto>(PondDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
