using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.BloodGroups;
using SchoolManagement.Application.Features.BloodGroups.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.BloodGroups.Handlers.Queries
{
    public class GetBloodGroupDetailRequestHandler : IRequestHandler<GetBloodGroupDetailRequest, BloodGroupDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<BloodGroup> _BloodGroupRepository;
        public GetBloodGroupDetailRequestHandler(ISchoolManagementRepository<BloodGroup> BloodGroupRepository, IMapper mapper)
        {
            _BloodGroupRepository = BloodGroupRepository;
            _mapper = mapper;
        }
        public async Task<BloodGroupDto> Handle(GetBloodGroupDetailRequest request, CancellationToken cancellationToken)
        {
            var BloodGroup = await _BloodGroupRepository.Get(request.BloodGroupId);
            return _mapper.Map<BloodGroupDto>(BloodGroup);
        }
    }
}
