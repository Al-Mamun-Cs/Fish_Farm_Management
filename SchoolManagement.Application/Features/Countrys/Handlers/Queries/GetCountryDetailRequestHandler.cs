using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Countrys;
using SchoolManagement.Application.Features.Countrys.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Countrys.Handlers.Queries
{
    public class GetCountryDetailRequestHandler : IRequestHandler<GetCountryDetailRequest, CountryDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Country> _CountryRepository;
        public GetCountryDetailRequestHandler(ISchoolManagementRepository<Country> CountryRepository, IMapper mapper)
        {
            _CountryRepository = CountryRepository;
            _mapper = mapper;
        }
        public async Task<CountryDto> Handle(GetCountryDetailRequest request, CancellationToken cancellationToken)
        {
            var Country = await _CountryRepository.Get(request.CountryId);
            return _mapper.Map<CountryDto>(Country);
        }
    }
}
