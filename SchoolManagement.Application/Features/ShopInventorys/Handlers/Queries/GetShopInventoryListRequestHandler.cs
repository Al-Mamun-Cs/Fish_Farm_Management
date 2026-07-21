using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.ShopInventorys;
using SchoolManagement.Application.Enum;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ShopInventorys.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System.Globalization;

namespace SchoolManagement.Application.Features.ShopInventorys.Handlers.Queries
{
    public class GetShopInventoryListRequestHandler : IRequestHandler<GetShopInventoryListRequest, PagedResult<ShopInventoryDto>>
    {

        private readonly ISchoolManagementRepository<ShopInventory> _ShopInventoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetShopInventoryListRequestHandler(ISchoolManagementRepository<ShopInventory> ShopInventoryRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _ShopInventoryRepository = ShopInventoryRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<ShopInventoryDto>> Handle(GetShopInventoryListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            DateTime searchDate;
            bool isDate = DateTime.TryParseExact(
                request.QueryParams.SearchText?.Trim(),
                "dd-MM-yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out searchDate);

            var startDate = searchDate.Date;
            var endDate = startDate.AddDays(1);

            IQueryable<ShopInventory> ShopInventorys = _ShopInventoryRepository.FilterWithInclude(x => (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) 
            && (x.Supplier.SupplierName.Contains(request.QueryParams.SearchText) || x.VoucherNo.Contains(request.QueryParams.SearchText) || (isDate &&
                 x.PurchaseDate.HasValue &&
                 x.PurchaseDate >= startDate &&
                 x.PurchaseDate < endDate) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Warehouse", "Supplier", "PaymentStatus");
            var totalCount = ShopInventorys.Count();
            ShopInventorys = ShopInventorys.OrderByDescending(x => x.ShopInventoryId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _ShopInventoryRepository.GetPermitedRoleFeatures(DeclareFeatureCode.SHOPINVENTORY, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var ShopInventoryDtos = _mapper.Map<List<ShopInventoryDto>>(ShopInventorys);
            var result = new PagedResult<ShopInventoryDto>(ShopInventoryDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
