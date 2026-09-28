using SchoolManagement.Application.Features.ProjectTypes.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.ProjectTypes;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.ProjectTypes.Handlers.Queries
{
    public class GetProjectTypeListRequestHandler : IRequestHandler<GetProjectTypeListRequest, PagedResult<ProjectTypeDto>>
    {

        private readonly ISchoolManagementRepository<ProjectType> _ProjectTypeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetProjectTypeListRequestHandler(ISchoolManagementRepository<ProjectType> ProjectTypeRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _ProjectTypeRepository = ProjectTypeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<ProjectTypeDto>> Handle(GetProjectTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<ProjectType> ProjectTypes = _ProjectTypeRepository.FilterWithInclude(x => (x.NameEnglish.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = ProjectTypes.Count();
            ProjectTypes = ProjectTypes.OrderByDescending(x => x.ProjectTypeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _ProjectTypeRepository.GetPermitedRoleFeatures(DeclareFeatureCode.PROJECTTYPE, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var ProjectTypeDtos = _mapper.Map<List<ProjectTypeDto>>(ProjectTypes);
            var result = new PagedResult<ProjectTypeDto>(ProjectTypeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
