using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Districts;
using SchoolManagement.Application.Features.Districts.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Districts.Handlers.Queries
{
    public class GetDistrictDetailRequestHandler : IRequestHandler<GetDistrictDetailRequest, DistrictDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<District> _DistrictRepository;
        public GetDistrictDetailRequestHandler(ISchoolManagementRepository<District> DistrictRepository, IMapper mapper)
        {
            _DistrictRepository = DistrictRepository;
            _mapper = mapper;
        }
        public async Task<DistrictDto> Handle(GetDistrictDetailRequest request, CancellationToken cancellationToken)
        {
            var District = await _DistrictRepository.Get(request.DistrictId);
            return _mapper.Map<DistrictDto>(District);
        }
    }
}
