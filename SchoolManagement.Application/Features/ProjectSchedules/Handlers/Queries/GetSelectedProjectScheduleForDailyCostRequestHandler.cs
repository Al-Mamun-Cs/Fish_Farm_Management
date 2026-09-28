using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ProjectSchedules.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ProjectSchedules.Handlers.Queries
{
    public class GetSelectedProjectScheduleForDailyCostRequestHandler : IRequestHandler<GetSelectedProjectScheduleForDailyCostRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ProjectSchedule> _ProjectScheduleRepository;


        public GetSelectedProjectScheduleForDailyCostRequestHandler(ISchoolManagementRepository<ProjectSchedule> ProjectScheduleRepository)
        {
            _ProjectScheduleRepository = ProjectScheduleRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedProjectScheduleForDailyCostRequest request, CancellationToken cancellationToken)
        {
            IQueryable<ProjectSchedule> codeValues =  _ProjectScheduleRepository.FilterWithInclude(x => x.ActiveStatus == 0);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = $"{x.Pond.NameBangla} - {x.Name}  {x.ProjectType.NameBangla} ",
                Value = x.ProjectScheduleId
            }).ToList();
            return selectModels;
        }
    }
}
