using SchoolManagement.Application.Features.FisheriesProductReturns.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.FisheriesProductReturns;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.FisheriesProductReturns.Handlers.Queries
{
    public class GetFisheriesProductReturnListRequestHandler : IRequestHandler<GetFisheriesProductReturnListRequest, PagedResult<FisheriesProductReturnDto>>
    {

        private readonly ISchoolManagementRepository<FisheriesProductReturn> _FisheriesProductReturnRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetFisheriesProductReturnListRequestHandler(ISchoolManagementRepository<FisheriesProductReturn> FisheriesProductReturnRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _FisheriesProductReturnRepository = FisheriesProductReturnRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<FisheriesProductReturnDto>> Handle(GetFisheriesProductReturnListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<FisheriesProductReturn> FisheriesProductReturns = _FisheriesProductReturnRepository.FilterWithInclude(x => (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) && (x.FisheriesInventoryDetail.ProductName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Warehouse", "Supplier", "FisheriesProductType", "FisheriesInventoryDetail");
            var totalCount = FisheriesProductReturns.Count();
            FisheriesProductReturns = FisheriesProductReturns.OrderByDescending(x => x.FisheriesProductReturnId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _FisheriesProductReturnRepository.GetPermitedRoleFeatures(DeclareFeatureCode.FISHERIESPRODUCTRETURN, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var FisheriesProductReturnDtos = _mapper.Map<List<FisheriesProductReturnDto>>(FisheriesProductReturns);
            var result = new PagedResult<FisheriesProductReturnDto>(FisheriesProductReturnDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
