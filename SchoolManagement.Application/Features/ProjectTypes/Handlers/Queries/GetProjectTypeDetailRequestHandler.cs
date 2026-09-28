using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ProjectTypes;
using SchoolManagement.Application.Features.ProjectTypes.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ProjectTypes.Handlers.Queries
{
    public class GetProjectTypeDetailRequestHandler : IRequestHandler<GetProjectTypeDetailRequest, ProjectTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ProjectType> _ProjectTypeRepository;
        public GetProjectTypeDetailRequestHandler(ISchoolManagementRepository<ProjectType> ProjectTypeRepository, IMapper mapper)
        {
            _ProjectTypeRepository = ProjectTypeRepository;
            _mapper = mapper;
        }
        public async Task<ProjectTypeDto> Handle(GetProjectTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var ProjectType = await _ProjectTypeRepository.Get(request.ProjectTypeId);
            return _mapper.Map<ProjectTypeDto>(ProjectType);
        }
    }
}
