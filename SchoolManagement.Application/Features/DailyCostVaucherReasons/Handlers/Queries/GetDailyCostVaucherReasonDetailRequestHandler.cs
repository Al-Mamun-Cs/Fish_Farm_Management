using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DailyCostVaucherReasons;
using SchoolManagement.Application.Features.DailyCostVaucherReasons.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DailyCostVaucherReasons.Handlers.Queries
{
    public class GetDailyCostVaucherReasonDetailRequestHandler : IRequestHandler<GetDailyCostVaucherReasonDetailRequest, DailyCostVaucherReasonDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<DailyCostVaucherReason> _DailyCostVaucherReasonRepository;
        public GetDailyCostVaucherReasonDetailRequestHandler(ISchoolManagementRepository<DailyCostVaucherReason> DailyCostVaucherReasonRepository, IMapper mapper)
        {
            _DailyCostVaucherReasonRepository = DailyCostVaucherReasonRepository;
            _mapper = mapper;
        }
        public async Task<DailyCostVaucherReasonDto> Handle(GetDailyCostVaucherReasonDetailRequest request, CancellationToken cancellationToken)
        {
            var DailyCostVaucherReason = await _DailyCostVaucherReasonRepository.Get(request.DailyCostVaucherReasonId);
            return _mapper.Map<DailyCostVaucherReasonDto>(DailyCostVaucherReason);
        }
    }
}
