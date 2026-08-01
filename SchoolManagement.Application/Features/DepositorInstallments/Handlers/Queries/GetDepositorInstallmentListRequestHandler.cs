using SchoolManagement.Application.Features.DepositorInstallments.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.DepositorInstallments;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.DepositorInstallments.Handlers.Queries
{
    public class GetDepositorInstallmentListRequestHandler : IRequestHandler<GetDepositorInstallmentListRequest, PagedResult<DepositorInstallmentDto>>
    {

        private readonly ISchoolManagementRepository<DepositorInstallment> _DepositorInstallmentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetDepositorInstallmentListRequestHandler(ISchoolManagementRepository<DepositorInstallment> DepositorInstallmentRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _DepositorInstallmentRepository = DepositorInstallmentRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<DepositorInstallmentDto>> Handle(GetDepositorInstallmentListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<DepositorInstallment> DepositorInstallments = _DepositorInstallmentRepository.FilterWithInclude(x => (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) && (x.Depositor.DepositorName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Warehouse", "Depositor");
            var totalCount = DepositorInstallments.Count();
            DepositorInstallments = DepositorInstallments.OrderByDescending(x => x.DepositorInstallmentId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _DepositorInstallmentRepository.GetPermitedRoleFeatures(DeclareFeatureCode.DEPOSITORINSTALLMENT, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var DepositorInstallmentDtos = _mapper.Map<List<DepositorInstallmentDto>>(DepositorInstallments);
            var result = new PagedResult<DepositorInstallmentDto>(DepositorInstallmentDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
