using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Fiscalyears;
using SchoolManagement.Application.Features.Fiscalyears.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Fiscalyears.Handlers.Queries
{
    public class GetFiscalyearDetailRequestHandler : IRequestHandler<GetFiscalyearDetailRequest, FiscalyearDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Fiscalyear> _FiscalyearRepository;
        public GetFiscalyearDetailRequestHandler(ISchoolManagementRepository<Fiscalyear> FiscalyearRepository, IMapper mapper)
        {
            _FiscalyearRepository = FiscalyearRepository;
            _mapper = mapper;
        }
        public async Task<FiscalyearDto> Handle(GetFiscalyearDetailRequest request, CancellationToken cancellationToken)
        {
            var Fiscalyear = await _FiscalyearRepository.Get(request.FiscalyearId);
            return _mapper.Map<FiscalyearDto>(Fiscalyear);
        }
    }
}
