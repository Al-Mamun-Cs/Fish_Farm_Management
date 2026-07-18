using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Countrys.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Countrys.Handlers.Queries
{
    public class GetAutoCompleteCountryRequestHandler : IRequestHandler<GetAutoCompleteCountryRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Country> _CountryRepository;
        public GetAutoCompleteCountryRequestHandler(ISchoolManagementRepository<Country> CountryRepository)
        {
            _CountryRepository = CountryRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetAutoCompleteCountryRequest request, CancellationToken cancellationToken)
        {
            ICollection<Country> traineeBioDataGeneralInfos = await _CountryRepository.FilterAsync(x => x.Name.Contains(request.Name));
            var selectModels = traineeBioDataGeneralInfos.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.CountryId
            }).ToList();
            return selectModels;
        }
    }
}
