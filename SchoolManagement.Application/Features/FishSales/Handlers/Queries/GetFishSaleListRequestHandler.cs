using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.FishSales;
using SchoolManagement.Application.Enum;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.FishSales.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System.Globalization;

namespace SchoolManagement.Application.Features.FishSales.Handlers.Queries
{
    public class GetFishSaleListRequestHandler : IRequestHandler<GetFishSaleListRequest, PagedResult<FishSaleDto>>
    {

        private readonly ISchoolManagementRepository<FishSale> _FishSaleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetFishSaleListRequestHandler(ISchoolManagementRepository<FishSale> FishSaleRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _FishSaleRepository = FishSaleRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<FishSaleDto>> Handle(GetFishSaleListRequest request, CancellationToken cancellationToken)
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

            IQueryable<FishSale> FishSales = _FishSaleRepository.FilterWithInclude(x => (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) && (x.Pond.NameBangla.Contains(request.QueryParams.SearchText)
            || (isDate && x.SaleDate.HasValue && x.SaleDate >= startDate && x.SaleDate < endDate) 
            || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Warehouse", "Pond", "Supplier", "FisheriesUnit", "PaymentStatus");
            var totalCount = FishSales.Count();
            FishSales = FishSales.OrderByDescending(x => x.FishSaleId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _FishSaleRepository.GetPermitedRoleFeatures(DeclareFeatureCode.FISHSALE, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var FishSaleDtos = _mapper.Map<List<FishSaleDto>>(FishSales);
            var result = new PagedResult<FishSaleDto>(FishSaleDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
