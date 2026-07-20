using SchoolManagement.Application.Features.ShopGoodSales.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.ShopGoodSales;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.ShopGoodSales.Handlers.Queries
{
    public class GetShopGoodSaleListRequestHandler : IRequestHandler<GetShopGoodSaleListRequest, PagedResult<ShopGoodSaleDto>>
    {

        private readonly ISchoolManagementRepository<ShopGoodSale> _ShopGoodSaleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetShopGoodSaleListRequestHandler(ISchoolManagementRepository<ShopGoodSale> ShopGoodSaleRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _ShopGoodSaleRepository = ShopGoodSaleRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<ShopGoodSaleDto>> Handle(GetShopGoodSaleListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<ShopGoodSale> ShopGoodSales = _ShopGoodSaleRepository.FilterWithInclude(x => (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) && (x.ApproveStatus == 0) && (x.Warehouse.WarehouseName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Warehouse", "Supplier", "PaymentStatus");
            var totalCount = ShopGoodSales.Count();
            ShopGoodSales = ShopGoodSales.OrderByDescending(x => x.ShopGoodSaleId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _ShopGoodSaleRepository.GetPermitedRoleFeatures(DeclareFeatureCode.SHOPGOODSALE, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var ShopGoodSaleDtos = _mapper.Map<List<ShopGoodSaleDto>>(ShopGoodSales);
            var result = new PagedResult<ShopGoodSaleDto>(ShopGoodSaleDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
