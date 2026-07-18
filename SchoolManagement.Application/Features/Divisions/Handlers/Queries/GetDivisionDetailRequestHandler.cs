using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Divisions;
using SchoolManagement.Application.Features.Divisions.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Divisions.Handlers.Queries
{
    public class GetDivisionDetailRequestHandler : IRequestHandler<GetDivisionDetailRequest, DivisionDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Division> _DivisionRepository;
        public GetDivisionDetailRequestHandler(ISchoolManagementRepository<Division> DivisionRepository, IMapper mapper)
        {
            _DivisionRepository = DivisionRepository;
            _mapper = mapper;
        }
        public async Task<DivisionDto> Handle(GetDivisionDetailRequest request, CancellationToken cancellationToken)
        {
            var Division = await _DivisionRepository.Get(request.DivisionId);
            return _mapper.Map<DivisionDto>(Division);
        }
    }
}
