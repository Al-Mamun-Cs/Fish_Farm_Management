using SchoolManagement.Application.Features.EasyBikeBanks.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.EasyBikeBanks;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.EasyBikeBanks.Handlers.Queries
{
    public class GetEasyBikeBankListRequestHandler : IRequestHandler<GetEasyBikeBankListRequest, PagedResult<EasyBikeBankDto>>
    {

        private readonly ISchoolManagementRepository<EasyBikeBank> _EasyBikeBankRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetEasyBikeBankListRequestHandler(ISchoolManagementRepository<EasyBikeBank> EasyBikeBankRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _EasyBikeBankRepository = EasyBikeBankRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<PagedResult<EasyBikeBankDto>> Handle(GetEasyBikeBankListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<EasyBikeBank> EasyBikeBanks = _EasyBikeBankRepository.FilterWithInclude(x => (x.BankName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "AccountType", "AccountName");
            var totalCount = EasyBikeBanks.Count();
            EasyBikeBanks = EasyBikeBanks.OrderByDescending(x => x.EasyBikeBankId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _EasyBikeBankRepository.GetPermitedRoleFeatures(DeclareFeatureCode.EASYBIKEBANK, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var EasyBikeBankDtos = _mapper.Map<List<EasyBikeBankDto>>(EasyBikeBanks);
            var result = new PagedResult<EasyBikeBankDto>(EasyBikeBankDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
