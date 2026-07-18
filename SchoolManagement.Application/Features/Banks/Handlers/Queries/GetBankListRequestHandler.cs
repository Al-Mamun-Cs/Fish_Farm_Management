using SchoolManagement.Application.Features.Banks.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Banks;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.Banks.Handlers.Queries
{
    public class GetBankListRequestHandler : IRequestHandler<GetBankListRequest, PagedResult<BankDto>>
    {

        private readonly ISchoolManagementRepository<Bank> _BankRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetBankListRequestHandler(ISchoolManagementRepository<Bank> BankRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _BankRepository = BankRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<BankDto>> Handle(GetBankListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<Bank> Banks = _BankRepository.FilterWithInclude(x => (x.BankName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Banks.Count();
            Banks = Banks.OrderByDescending(x => x.BankId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _BankRepository.GetPermitedRoleFeatures(DeclareFeatureCode.BANK, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var BankDtos = _mapper.Map<List<BankDto>>(Banks);
            var result = new PagedResult<BankDto>(BankDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
