using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Fiscalyears.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Fiscalyears.Handlers.Queries
{
    public class GetSelectedFiscalyearRequestHandler : IRequestHandler<GetSelectedFiscalyearRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Fiscalyear> _FiscalyearRepository;


        public GetSelectedFiscalyearRequestHandler(ISchoolManagementRepository<Fiscalyear> FiscalyearRepository)
        {
            _FiscalyearRepository = FiscalyearRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedFiscalyearRequest request, CancellationToken cancellationToken)
        {
            ICollection<Fiscalyear> codeValues = await _FiscalyearRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.FiscalyearId
            }).ToList();
            return selectModels;
        }
    }
}
