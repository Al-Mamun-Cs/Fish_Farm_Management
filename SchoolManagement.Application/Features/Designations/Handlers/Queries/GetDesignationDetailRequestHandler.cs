using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Designations;
using SchoolManagement.Application.Features.Designations.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Designations.Handlers.Queries
{
    public class GetDesignationDetailRequestHandler : IRequestHandler<GetDesignationDetailRequest, DesignationDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Designation> _DesignationRepository;
        public GetDesignationDetailRequestHandler(ISchoolManagementRepository<Designation> DesignationRepository, IMapper mapper)
        {
            _DesignationRepository = DesignationRepository;
            _mapper = mapper;
        }
        public async Task<DesignationDto> Handle(GetDesignationDetailRequest request, CancellationToken cancellationToken)
        {
            var Designation = await _DesignationRepository.Get(request.DesignationId);
            return _mapper.Map<DesignationDto>(Designation);
        }
    }
}
