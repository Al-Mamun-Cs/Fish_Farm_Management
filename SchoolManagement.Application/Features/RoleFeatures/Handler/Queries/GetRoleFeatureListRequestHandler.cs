using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.RoleFeature;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Features.RoleFeatures.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.RoleFeatures.Handler.Queries
{
    public class GetRoleFeatureListRequestHandler : IRequestHandler<GetRoleFeatureListRequest, PagedResult<RoleFeatureDto>>
    { 

        private readonly ISchoolManagementRepository<RoleFeature> _branchRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetRoleFeatureListRequestHandler(ISchoolManagementRepository<RoleFeature> branchRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<PagedResult<RoleFeatureDto>> Handle(GetRoleFeatureListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult.ToString());

            IQueryable<RoleFeature> branches = _branchRepository.FilterWithInclude(x => String.IsNullOrEmpty(request.QueryParams.SearchText));
            var totalCount = branches.Count();
            branches = branches.OrderByDescending(x => x.RoleId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _branchRepository.GetPermitedRoleFeatures(DeclareFeatureCode.FEATURE, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var branchesDtos = _mapper.Map<List<RoleFeatureDto>>(branches);
            var result = new PagedResult<RoleFeatureDto>(branchesDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;
        }
    }
}
