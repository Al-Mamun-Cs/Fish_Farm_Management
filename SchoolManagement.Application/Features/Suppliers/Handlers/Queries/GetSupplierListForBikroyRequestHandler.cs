using SchoolManagement.Application.Features.Suppliers.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Suppliers;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.Suppliers.Handlers.Queries
{
    public class GetSupplierListForBikroyRequestHandler : IRequestHandler<GetSupplierListForBikroyRequest, PagedResult<SupplierDto>>
    {

        private readonly ISchoolManagementRepository<Supplier> _SupplierRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetSupplierListForBikroyRequestHandler(ISchoolManagementRepository<Supplier> SupplierRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _SupplierRepository = SupplierRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<SupplierDto>> Handle(GetSupplierListForBikroyRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Supplier> Suppliers = _SupplierRepository.FilterWithInclude(x => (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) && (x.SupplierName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText) && x.SupplierStatus==2), "Warehouse", "SupplierType", "Division", "District", "Upozila");
            var totalCount = Suppliers.Count();
            Suppliers = Suppliers.OrderByDescending(x => x.SupplierId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _SupplierRepository.GetPermitedRoleFeatures(DeclareFeatureCode.SUPPLIER, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var SupplierDtos = _mapper.Map<List<SupplierDto>>(Suppliers);
            var result = new PagedResult<SupplierDto>(SupplierDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
