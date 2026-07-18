using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Genders;
using SchoolManagement.Application.Features.Genders.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Genders.Handlers.Queries
{
    public class GetGenderDetailRequestHandler : IRequestHandler<GetGenderDetailRequest, GenderDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Gender> _GenderRepository;
        public GetGenderDetailRequestHandler(ISchoolManagementRepository<Gender> GenderRepository, IMapper mapper)
        {
            _GenderRepository = GenderRepository;
            _mapper = mapper;
        }
        public async Task<GenderDto> Handle(GetGenderDetailRequest request, CancellationToken cancellationToken)
        {
            var Gender = await _GenderRepository.Get(request.GenderId);
            return _mapper.Map<GenderDto>(Gender);
        }
    }
}
