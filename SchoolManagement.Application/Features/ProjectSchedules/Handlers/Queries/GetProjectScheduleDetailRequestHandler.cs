using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ProjectSchedules;
using SchoolManagement.Application.Features.ProjectSchedules.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ProjectSchedules.Handlers.Queries
{
    public class GetProjectScheduleDetailRequestHandler : IRequestHandler<GetProjectScheduleDetailRequest, ProjectScheduleDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ProjectSchedule> _ProjectScheduleRepository;
        public GetProjectScheduleDetailRequestHandler(ISchoolManagementRepository<ProjectSchedule> ProjectScheduleRepository, IMapper mapper)
        {
            _ProjectScheduleRepository = ProjectScheduleRepository;
            _mapper = mapper;
        }
        public async Task<ProjectScheduleDto> Handle(GetProjectScheduleDetailRequest request, CancellationToken cancellationToken)
        {
            var ProjectSchedule = await _ProjectScheduleRepository.Get(request.ProjectScheduleId);
            return _mapper.Map<ProjectScheduleDto>(ProjectSchedule);
        }
    }
}
