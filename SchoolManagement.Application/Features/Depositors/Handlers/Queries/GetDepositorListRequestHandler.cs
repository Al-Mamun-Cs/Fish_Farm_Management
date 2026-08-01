using SchoolManagement.Application.Features.Depositors.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Depositors;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.Depositors.Handlers.Queries
{
    public class GetDepositorListRequestHandler : IRequestHandler<GetDepositorListRequest, PagedResult<DepositorDto>>
    {

        private readonly ISchoolManagementRepository<Depositor> _DepositorRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetDepositorListRequestHandler(ISchoolManagementRepository<Depositor> DepositorRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _DepositorRepository = DepositorRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<DepositorDto>> Handle(GetDepositorListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Depositor> Depositors = _DepositorRepository.FilterWithInclude(x => (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) && (x.DepositorName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Warehouse");
            var totalCount = Depositors.Count();
            Depositors = Depositors.OrderByDescending(x => x.DepositorId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _DepositorRepository.GetPermitedRoleFeatures(DeclareFeatureCode.DEPOSITOR, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var DepositorDtos = _mapper.Map<List<DepositorDto>>(Depositors);
            var result = new PagedResult<DepositorDto>(DepositorDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
