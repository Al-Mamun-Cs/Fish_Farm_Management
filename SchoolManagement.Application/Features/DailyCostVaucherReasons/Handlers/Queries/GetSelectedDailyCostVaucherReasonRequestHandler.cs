using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.DailyCostVaucherReasons.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.DailyCostVaucherReasons.Handlers.Queries
{
    public class GetSelectedDailyCostVaucherReasonRequestHandler : IRequestHandler<GetSelectedDailyCostVaucherReasonRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<DailyCostVaucherReason> _DailyCostVaucherReasonRepository;


        public GetSelectedDailyCostVaucherReasonRequestHandler(ISchoolManagementRepository<DailyCostVaucherReason> DailyCostVaucherReasonRepository)
        {
            _DailyCostVaucherReasonRepository = DailyCostVaucherReasonRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDailyCostVaucherReasonRequest request, CancellationToken cancellationToken)
        {
            ICollection<DailyCostVaucherReason> codeValues = await _DailyCostVaucherReasonRepository.FilterAsync(x => x.WarehouseId == request.WarehouseId);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.FullName,
                Value = x.DailyCostVaucherReasonId
            }).ToList();
            return selectModels;
        }
    }
}
