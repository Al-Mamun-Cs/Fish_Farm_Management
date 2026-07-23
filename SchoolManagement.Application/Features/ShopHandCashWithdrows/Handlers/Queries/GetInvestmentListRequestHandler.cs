using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.DTOs.ShopHandCashWithdrows;
using SchoolManagement.Application.Enum;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ShopHandCashWithdrows.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using System.Globalization;

namespace SchoolManagement.Application.Features.ShopHandCashWithdrows.Handlers.Queries
{
    public class GetInvestmentListRequestHandler : IRequestHandler<GetInvestmentListRequest, PagedResult<ShopHandCashWithdrowDto>>
    {

        private readonly ISchoolManagementRepository<ShopHandCashWithdrow> _ShopHandCashWithdrowRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetInvestmentListRequestHandler(ISchoolManagementRepository<ShopHandCashWithdrow> ShopHandCashWithdrowRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _ShopHandCashWithdrowRepository = ShopHandCashWithdrowRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<ShopHandCashWithdrowDto>> Handle(GetInvestmentListRequest request, CancellationToken cancellationToken)
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

            IQueryable<ShopHandCashWithdrow> ShopHandCashWithdrows = _ShopHandCashWithdrowRepository.FilterWithInclude(x => (x.Type == 2) && (x.TransferReason.Contains(request.QueryParams.SearchText) 
            || (isDate && x.TransferDate.HasValue && x.TransferDate >= startDate && x.TransferDate < endDate) 
            || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Warehouse");
            var totalCount = ShopHandCashWithdrows.Count();
            ShopHandCashWithdrows = ShopHandCashWithdrows.OrderByDescending(x => x.ShopHandCashWithdrowId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _ShopHandCashWithdrowRepository.GetPermitedRoleFeatures(DeclareFeatureCode.INVESTMENT, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var ShopHandCashWithdrowDtos = _mapper.Map<List<ShopHandCashWithdrowDto>>(ShopHandCashWithdrows);
            var result = new PagedResult<ShopHandCashWithdrowDto>(ShopHandCashWithdrowDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
