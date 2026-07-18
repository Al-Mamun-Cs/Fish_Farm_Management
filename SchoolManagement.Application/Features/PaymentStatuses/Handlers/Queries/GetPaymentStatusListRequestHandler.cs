using SchoolManagement.Application.Features.PaymentStatuses.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.PaymentStatuses;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.PaymentStatuses.Handlers.Queries
{
    public class GetPaymentStatusListRequestHandler : IRequestHandler<GetPaymentStatusListRequest, PagedResult<PaymentStatusDto>>
    {

        private readonly ISchoolManagementRepository<PaymentStatus> _PaymentStatusRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetPaymentStatusListRequestHandler(ISchoolManagementRepository<PaymentStatus> PaymentStatusRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _PaymentStatusRepository = PaymentStatusRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<PaymentStatusDto>> Handle(GetPaymentStatusListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<PaymentStatus> PaymentStatuss = _PaymentStatusRepository.FilterWithInclude(x => (x.StatusName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = PaymentStatuss.Count();
            PaymentStatuss = PaymentStatuss.OrderByDescending(x => x.PaymentStatusId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _PaymentStatusRepository.GetPermitedRoleFeatures(DeclareFeatureCode.PAYMENTSTATUS, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var PaymentStatusDtos = _mapper.Map<List<PaymentStatusDto>>(PaymentStatuss);
            var result = new PagedResult<PaymentStatusDto>(PaymentStatusDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
