using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Upozilas;
using SchoolManagement.Application.Features.Upozilas.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Upozilas.Handlers.Queries
{
    public class GetUpozilaDetailRequestHandler : IRequestHandler<GetUpozilaDetailRequest, UpozilaDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Upozila> _UpozilaRepository;
        public GetUpozilaDetailRequestHandler(ISchoolManagementRepository<Upozila> UpozilaRepository, IMapper mapper)
        {
            _UpozilaRepository = UpozilaRepository;
            _mapper = mapper;
        }
        public async Task<UpozilaDto> Handle(GetUpozilaDetailRequest request, CancellationToken cancellationToken)
        {
            var Upozila = await _UpozilaRepository.Get(request.UpazilaId);
            return _mapper.Map<UpozilaDto>(Upozila);
        }
    }
}
