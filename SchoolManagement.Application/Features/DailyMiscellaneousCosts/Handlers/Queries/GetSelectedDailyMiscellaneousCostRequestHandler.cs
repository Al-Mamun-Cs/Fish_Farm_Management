using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Handlers.Queries
{
    public class GetSelectedDailyMiscellaneousCostRequestHandler : IRequestHandler<GetSelectedDailyMiscellaneousCostRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<DailyMiscellaneousCost> _DailyMiscellaneousCostRepository;


        public GetSelectedDailyMiscellaneousCostRequestHandler(ISchoolManagementRepository<DailyMiscellaneousCost> DailyMiscellaneousCostRepository)
        {
            _DailyMiscellaneousCostRepository = DailyMiscellaneousCostRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDailyMiscellaneousCostRequest request, CancellationToken cancellationToken)
        {
            ICollection<DailyMiscellaneousCost> codeValues = await _DailyMiscellaneousCostRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.DailyCostVaucherReason.FullName,
                Value = x.DailyMiscellaneousCostId
            }).ToList();
            return selectModels;
        }
    }
}
