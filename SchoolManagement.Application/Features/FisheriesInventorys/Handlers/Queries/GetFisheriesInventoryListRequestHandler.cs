using SchoolManagement.Application.Features.FisheriesInventorys.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.FisheriesInventorys;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;
using System.Globalization;

namespace SchoolManagement.Application.Features.FisheriesInventorys.Handlers.Queries
{
    public class GetFisheriesInventoryListRequestHandler : IRequestHandler<GetFisheriesInventoryListRequest, PagedResult<FisheriesInventoryDto>>
    {

        private readonly ISchoolManagementRepository<FisheriesInventory> _FisheriesInventoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetFisheriesInventoryListRequestHandler(ISchoolManagementRepository<FisheriesInventory> FisheriesInventoryRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _FisheriesInventoryRepository = FisheriesInventoryRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<FisheriesInventoryDto>> Handle(GetFisheriesInventoryListRequest request, CancellationToken cancellationToken)
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

            IQueryable<FisheriesInventory> FisheriesInventorys = _FisheriesInventoryRepository.FilterWithInclude(x => (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) 
            && (x.Supplier.SupplierName.Contains(request.QueryParams.SearchText) || x.VoucherNo.Contains(request.QueryParams.SearchText) || (isDate &&
                 x.PurchaseDate.HasValue &&
                 x.PurchaseDate >= startDate &&
                 x.PurchaseDate < endDate) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Warehouse", "Supplier", "PaymentStatus");
            var totalCount = FisheriesInventorys.Count();
            FisheriesInventorys = FisheriesInventorys.OrderByDescending(x => x.PurchaseDate).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _FisheriesInventoryRepository.GetPermitedRoleFeatures(DeclareFeatureCode.FISHERIESINVENTORY, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var FisheriesInventoryDtos = _mapper.Map<List<FisheriesInventoryDto>>(FisheriesInventorys);
            var result = new PagedResult<FisheriesInventoryDto>(FisheriesInventoryDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
