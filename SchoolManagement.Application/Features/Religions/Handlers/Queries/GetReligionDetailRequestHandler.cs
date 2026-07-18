using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Religions;
using SchoolManagement.Application.Features.Religions.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Religions.Handlers.Queries
{
    public class GetReligionDetailRequestHandler : IRequestHandler<GetReligionDetailRequest, ReligionDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Religion> _ReligionRepository;
        public GetReligionDetailRequestHandler(ISchoolManagementRepository<Religion> ReligionRepository, IMapper mapper)
        {
            _ReligionRepository = ReligionRepository;
            _mapper = mapper;
        }
        public async Task<ReligionDto> Handle(GetReligionDetailRequest request, CancellationToken cancellationToken)
        {
            var Religion = await _ReligionRepository.Get(request.ReligionId);
            return _mapper.Map<ReligionDto>(Religion);
        }
    }
}
