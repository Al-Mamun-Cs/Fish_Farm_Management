using SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.FisheriesInventoryOuts;
using SchoolManagement.Domain;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Enum;

namespace SchoolManagement.Application.Features.FisheriesInventoryOuts.Handlers.Queries
{
    public class GetFisheriesInventoryOutListRequestHandler : IRequestHandler<GetFisheriesInventoryOutListRequest, PagedResult<FisheriesInventoryOutDto>>
    {

        private readonly ISchoolManagementRepository<FisheriesInventoryOut> _FisheriesInventoryOutRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public GetFisheriesInventoryOutListRequestHandler(ISchoolManagementRepository<FisheriesInventoryOut> FisheriesInventoryOutRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _FisheriesInventoryOutRepository = FisheriesInventoryOutRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResult<FisheriesInventoryOutDto>> Handle(GetFisheriesInventoryOutListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<FisheriesInventoryOut> FisheriesInventoryOuts = _FisheriesInventoryOutRepository.FilterWithInclude(x => (x.Pond.NameEnglish.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "Warehouse", "Pond", "FisheriesInventoryDetail", "FisheriesProductType");
            var totalCount = FisheriesInventoryOuts.Count();
            FisheriesInventoryOuts = FisheriesInventoryOuts.OrderByDescending(x => x.FisheriesInventoryOutId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);
            var permission = _FisheriesInventoryOutRepository.GetPermitedRoleFeatures(DeclareFeatureCode.FISHERIESINVENTORYOUT, _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Rid)?.Value);
            var FisheriesInventoryOutDtos = _mapper.Map<List<FisheriesInventoryOutDto>>(FisheriesInventoryOuts);
            var result = new PagedResult<FisheriesInventoryOutDto>(FisheriesInventoryOutDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize, permission);

            return result;


        }
    }
}
