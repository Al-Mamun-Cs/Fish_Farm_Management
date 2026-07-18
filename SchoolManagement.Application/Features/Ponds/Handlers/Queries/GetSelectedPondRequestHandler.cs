using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Ponds.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Ponds.Handlers.Queries
{
    public class GetSelectedPondRequestHandler : IRequestHandler<GetSelectedPondRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Pond> _PondRepository;


        public GetSelectedPondRequestHandler(ISchoolManagementRepository<Pond> PondRepository)
        {
            _PondRepository = PondRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedPondRequest request, CancellationToken cancellationToken)
        {
            ICollection<Pond> codeValues = await _PondRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.NameBangla + " - " + x.NameEnglish,
                Value = x.PondId
            }).ToList();
            return selectModels;
        }
    }
}
