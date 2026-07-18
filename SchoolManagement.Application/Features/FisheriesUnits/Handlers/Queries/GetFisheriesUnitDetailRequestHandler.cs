using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FisheriesUnits;
using SchoolManagement.Application.Features.FisheriesUnits.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesUnits.Handlers.Queries
{
    public class GetFisheriesUnitDetailRequestHandler : IRequestHandler<GetFisheriesUnitDetailRequest, FisheriesUnitDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<FisheriesUnit> _FisheriesUnitRepository;
        public GetFisheriesUnitDetailRequestHandler(ISchoolManagementRepository<FisheriesUnit> FisheriesUnitRepository, IMapper mapper)
        {
            _FisheriesUnitRepository = FisheriesUnitRepository;
            _mapper = mapper;
        }
        public async Task<FisheriesUnitDto> Handle(GetFisheriesUnitDetailRequest request, CancellationToken cancellationToken)
        {
            var FisheriesUnit = await _FisheriesUnitRepository.Get(request.FisheriesUnitId);
            return _mapper.Map<FisheriesUnitDto>(FisheriesUnit);
        }
    }
}
