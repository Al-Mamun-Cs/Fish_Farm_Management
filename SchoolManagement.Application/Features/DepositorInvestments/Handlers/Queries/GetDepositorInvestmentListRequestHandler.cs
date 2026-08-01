using SchoolManagement.Application.Features.DepositorInvestments.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.DepositorInvestments;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.DepositorInvestments.Handlers.Queries
{
    public class GetDepositorInvestmentListRequestHandler : IRequestHandler<GetDepositorInvestmentListRequest, PagedResult<DepositorInvestmentDto>>
    {

        private readonly ISchoolManagementRepository<DepositorInvestment> _DepositorInvestmentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetDepositorInvestmentListRequestHandler(ISchoolManagementRepository<DepositorInvestment> DepositorInvestmentRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _DepositorInvestmentRepository = DepositorInvestmentRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<DepositorInvestmentDto>> Handle(GetDepositorInvestmentListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<DepositorInvestment> DepositorInvestments = _DepositorInvestmentRepository.FilterWithInclude(x => (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) && (x.BusinessOperatorName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Warehouse", "Depositor");
            var totalCount = DepositorInvestments.Count();
            DepositorInvestments = DepositorInvestments.OrderByDescending(x => x.DepositorInvestmentId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _DepositorInvestmentRepository.GetPermitedRoleFeatures(DeclareFeatureCode.DEPOSITORINVESTMENT, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var DepositorInvestmentDtos = _mapper.Map<List<DepositorInvestmentDto>>(DepositorInvestments);
            var result = new PagedResult<DepositorInvestmentDto>(DepositorInvestmentDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
