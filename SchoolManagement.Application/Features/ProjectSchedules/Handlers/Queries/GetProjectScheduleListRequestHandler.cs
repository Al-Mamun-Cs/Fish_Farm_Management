using SchoolManagement.Application.Features.ProjectSchedules.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.ProjectSchedules;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.ProjectSchedules.Handlers.Queries
{
    public class GetProjectScheduleListRequestHandler : IRequestHandler<GetProjectScheduleListRequest, PagedResult<ProjectScheduleDto>>
    {

        private readonly ISchoolManagementRepository<ProjectSchedule> _ProjectScheduleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetProjectScheduleListRequestHandler(ISchoolManagementRepository<ProjectSchedule> ProjectScheduleRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _ProjectScheduleRepository = ProjectScheduleRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<ProjectScheduleDto>> Handle(GetProjectScheduleListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<ProjectSchedule> ProjectSchedules = _ProjectScheduleRepository.FilterWithInclude(x => (x.Pond.NameBangla.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Warehouse", "Pond");
            var totalCount = ProjectSchedules.Count();
            ProjectSchedules = ProjectSchedules.OrderBy(x => x.ActiveStatus).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _ProjectScheduleRepository.GetPermitedRoleFeatures(DeclareFeatureCode.PROJECTSCHEDULE, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var ProjectScheduleDtos = _mapper.Map<List<ProjectScheduleDto>>(ProjectSchedules);
            var result = new PagedResult<ProjectScheduleDto>(ProjectScheduleDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
