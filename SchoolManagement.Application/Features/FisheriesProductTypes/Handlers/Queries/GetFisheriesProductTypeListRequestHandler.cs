using SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.FisheriesProductTypes;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Handlers.Queries
{
    public class GetFisheriesProductTypeListRequestHandler : IRequestHandler<GetFisheriesProductTypeListRequest, PagedResult<FisheriesProductTypeDto>>
    {

        private readonly ISchoolManagementRepository<FisheriesProductType> _FisheriesProductTypeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetFisheriesProductTypeListRequestHandler(ISchoolManagementRepository<FisheriesProductType> FisheriesProductTypeRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _FisheriesProductTypeRepository = FisheriesProductTypeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<FisheriesProductTypeDto>> Handle(GetFisheriesProductTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<FisheriesProductType> FisheriesProductTypes = _FisheriesProductTypeRepository.FilterWithInclude(x => (x.NameEnglish.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = FisheriesProductTypes.Count();
            FisheriesProductTypes = FisheriesProductTypes.OrderByDescending(x => x.FisheriesProductTypeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _FisheriesProductTypeRepository.GetPermitedRoleFeatures(DeclareFeatureCode.FISHERIESPRODUCTTYPE, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var FisheriesProductTypeDtos = _mapper.Map<List<FisheriesProductTypeDto>>(FisheriesProductTypes);
            var result = new PagedResult<FisheriesProductTypeDto>(FisheriesProductTypeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
