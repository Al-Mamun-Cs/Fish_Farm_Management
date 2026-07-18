using SchoolManagement.Application.Features.Divisions.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Divisions;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.Divisions.Handlers.Queries
{
    public class GetDivisionListRequestHandler : IRequestHandler<GetDivisionListRequest, PagedResult<DivisionDto>>
    {

        private readonly ISchoolManagementRepository<Division> _DivisionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetDivisionListRequestHandler(ISchoolManagementRepository<Division> DivisionRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _DivisionRepository = DivisionRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<DivisionDto>> Handle(GetDivisionListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Division> Divisions = _DivisionRepository.FilterWithInclude(x => (x.DivisionName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Divisions.Count();
            Divisions = Divisions.OrderByDescending(x => x.DivisionId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _DivisionRepository.GetPermitedRoleFeatures(DeclareFeatureCode.Division, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var DivisionDtos = _mapper.Map<List<DivisionDto>>(Divisions);
            var result = new PagedResult<DivisionDto>(DivisionDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
