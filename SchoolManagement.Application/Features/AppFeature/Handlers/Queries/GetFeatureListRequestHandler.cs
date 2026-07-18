using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.Features;
using SchoolManagement.Application.Enum;
using SchoolManagement.Application.Features.AppFeature.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.AppFeature.Handlers.Queries
{
    public class GetFeatureListRequestHandler : IRequestHandler<GetFeatureListRequest, PagedResult<FeatureDto>>
    { 

        private readonly ISchoolManagementRepository<Feature> _FeatureRepository;    

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetFeatureListRequestHandler(ISchoolManagementRepository<Feature> FeatureRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _FeatureRepository = FeatureRepository; 
            _mapper = mapper; 
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<FeatureDto>> Handle(GetFeatureListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.ToString()); 

            IQueryable<Feature> Features = _FeatureRepository.FilterWithInclude(x => (x.FeatureName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Module");
            var totalCount = Features.Count();
            Features = Features.OrderBy(x => x.FeatureId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _FeatureRepository.GetPermitedRoleFeatures(DeclareFeatureCode.FEATURE, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var FeaturesDtos = _mapper.Map<List<FeatureDto>>(Features); 
            var result = new PagedResult<FeatureDto>(FeaturesDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;
        }
    }
}
