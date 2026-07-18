using SchoolManagement.Application.Features.Designations.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Designations;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.Designations.Handlers.Queries
{
    public class GetWareHouseSubDepartmentListRequestHandler : IRequestHandler<GetDesignationListRequest, PagedResult<DesignationDto>>
    {

        private readonly ISchoolManagementRepository<Designation> _DesignationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetWareHouseSubDepartmentListRequestHandler(ISchoolManagementRepository<Designation> DesignationRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _DesignationRepository = DesignationRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<DesignationDto>> Handle(GetDesignationListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Designation> Designations = _DesignationRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Warehouse");
            var totalCount = Designations.Count();
            Designations = Designations.OrderBy(x => x.MenuPosition).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _DesignationRepository.GetPermitedRoleFeatures(DeclareFeatureCode.DESIGNATION, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var DesignationDtos = _mapper.Map<List<DesignationDto>>(Designations);
            var result = new PagedResult<DesignationDto>(DesignationDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
