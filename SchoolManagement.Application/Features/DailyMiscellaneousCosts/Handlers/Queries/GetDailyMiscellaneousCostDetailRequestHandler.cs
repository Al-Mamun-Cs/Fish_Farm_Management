using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DailyMiscellaneousCosts;
using SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Handlers.Queries
{
    public class GetDailyMiscellaneousCostDetailRequestHandler : IRequestHandler<GetDailyMiscellaneousCostDetailRequest, DailyMiscellaneousCostDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<DailyMiscellaneousCost> _DailyMiscellaneousCostRepository;
        public GetDailyMiscellaneousCostDetailRequestHandler(ISchoolManagementRepository<DailyMiscellaneousCost> DailyMiscellaneousCostRepository, IMapper mapper)
        {
            _DailyMiscellaneousCostRepository = DailyMiscellaneousCostRepository;
            _mapper = mapper;
        }
        public async Task<DailyMiscellaneousCostDto> Handle(GetDailyMiscellaneousCostDetailRequest request, CancellationToken cancellationToken)
        {
            var DailyMiscellaneousCost = await _DailyMiscellaneousCostRepository.Get(request.DailyMiscellaneousCostId);
            return _mapper.Map<DailyMiscellaneousCostDto>(DailyMiscellaneousCost);
        }
    }
}
