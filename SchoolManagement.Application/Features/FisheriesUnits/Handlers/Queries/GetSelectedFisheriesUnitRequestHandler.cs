using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.FisheriesUnits.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.FisheriesUnits.Handlers.Queries
{
    public class GetSelectedFisheriesUnitRequestHandler : IRequestHandler<GetSelectedFisheriesUnitRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<FisheriesUnit> _FisheriesUnitRepository;


        public GetSelectedFisheriesUnitRequestHandler(ISchoolManagementRepository<FisheriesUnit> FisheriesUnitRepository)
        {
            _FisheriesUnitRepository = FisheriesUnitRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedFisheriesUnitRequest request, CancellationToken cancellationToken)
        {
            ICollection<FisheriesUnit> codeValues = await _FisheriesUnitRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.FullName,
                Value = x.FisheriesUnitId
            }).ToList();
            return selectModels;
        }
    }
}
