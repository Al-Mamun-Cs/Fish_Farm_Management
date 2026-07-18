using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Countrys.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Countrys.Handlers.Queries
{
    public class GetSelectedCountryRequestHandler : IRequestHandler<GetSelectedCountryRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Country> _CountryRepository;


        public GetSelectedCountryRequestHandler(ISchoolManagementRepository<Country> CountryRepository)
        {
            _CountryRepository = CountryRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedCountryRequest request, CancellationToken cancellationToken)
        {
            ICollection<Country> codeValues = await _CountryRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.CountryId
            }).ToList();
            return selectModels;
        }
    }
}
