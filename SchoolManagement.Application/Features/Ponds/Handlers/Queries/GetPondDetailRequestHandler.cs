using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Ponds;
using SchoolManagement.Application.Features.Ponds.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Ponds.Handlers.Queries
{
    public class GetPondDetailRequestHandler : IRequestHandler<GetPondDetailRequest, PondDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Pond> _PondRepository;
        public GetPondDetailRequestHandler(ISchoolManagementRepository<Pond> PondRepository, IMapper mapper)
        {
            _PondRepository = PondRepository;
            _mapper = mapper;
        }
        public async Task<PondDto> Handle(GetPondDetailRequest request, CancellationToken cancellationToken)
        {
            var Pond = await _PondRepository.Get(request.PondId);
            return _mapper.Map<PondDto>(Pond);
        }
    }
}
