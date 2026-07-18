using SchoolManagement.Application.Features.SupplierTypes.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.SupplierTypes;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.SupplierTypes.Handlers.Queries
{
    public class GetSupplierTypeListRequestHandler : IRequestHandler<GetSupplierTypeListRequest, PagedResult<SupplierTypeDto>>
    {

        private readonly ISchoolManagementRepository<SupplierType> _SupplierTypeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetSupplierTypeListRequestHandler(ISchoolManagementRepository<SupplierType> SupplierTypeRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _SupplierTypeRepository = SupplierTypeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<SupplierTypeDto>> Handle(GetSupplierTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SupplierType> SupplierTypes = _SupplierTypeRepository.FilterWithInclude(x => (x.SupplierTypeName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = SupplierTypes.Count();
            SupplierTypes = SupplierTypes.OrderByDescending(x => x.SupplierTypeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _SupplierTypeRepository.GetPermitedRoleFeatures(DeclareFeatureCode.SUPPLIERTYPE, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var SupplierTypeDtos = _mapper.Map<List<SupplierTypeDto>>(SupplierTypes);
            var result = new PagedResult<SupplierTypeDto>(SupplierTypeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
