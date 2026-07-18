using SchoolManagement.Application.Features.Upozilas.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Upozilas;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.Upozilas.Handlers.Queries
{
    public class GetUpozilaListRequestHandler : IRequestHandler<GetUpozilaListRequest, PagedResult<UpozilaDto>>
    {

        private readonly ISchoolManagementRepository<Upozila> _UpozilaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetUpozilaListRequestHandler(ISchoolManagementRepository<Upozila> UpozilaRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _UpozilaRepository = UpozilaRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<UpozilaDto>> Handle(GetUpozilaListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Upozila> Upozilas = _UpozilaRepository.FilterWithInclude(x => (x.UpazilaName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "District");
            var totalCount = Upozilas.Count();
            Upozilas = Upozilas.OrderByDescending(x => x.UpazilaId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _UpozilaRepository.GetPermitedRoleFeatures(DeclareFeatureCode.UPOZILA, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var UpozilaDtos = _mapper.Map<List<UpozilaDto>>(Upozilas);
            var result = new PagedResult<UpozilaDto>(UpozilaDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
