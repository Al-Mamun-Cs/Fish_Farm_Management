using SchoolManagement.Application.Features.Warehouses.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Warehouses;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.Warehouses.Handlers.Queries
{
    public class GetWarehouseListRequestHandler : IRequestHandler<GetWarehouseListRequest, PagedResult<WarehouseDto>>
    {

        private readonly ISchoolManagementRepository<Warehouse> _WarehouseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetWarehouseListRequestHandler(ISchoolManagementRepository<Warehouse> WarehouseRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _WarehouseRepository = WarehouseRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<WarehouseDto>> Handle(GetWarehouseListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Warehouse> Warehouses = _WarehouseRepository.FilterWithInclude(x => (x.WarehouseName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Warehouses.Count();
            Warehouses = Warehouses.OrderByDescending(x => x.WarehouseId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _WarehouseRepository.GetPermitedRoleFeatures(DeclareFeatureCode.WAREHOUSE, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var WarehouseDtos = _mapper.Map<List<WarehouseDto>>(Warehouses);
            var result = new PagedResult<WarehouseDto>(WarehouseDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
