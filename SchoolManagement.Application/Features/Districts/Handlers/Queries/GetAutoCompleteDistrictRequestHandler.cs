using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Districts.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Districts.Handlers.Queries
{
    public class GetAutoCompleteDistrictRequestHandler : IRequestHandler<GetAutoCompleteDistrictRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<District> _DistrictRepository;
        public GetAutoCompleteDistrictRequestHandler(ISchoolManagementRepository<District> DistrictRepository)
        {
            _DistrictRepository = DistrictRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetAutoCompleteDistrictRequest request, CancellationToken cancellationToken)
        {
            ICollection<District> traineeBioDataGeneralInfos = await _DistrictRepository.FilterAsync(x => x.DistrictName.Contains(request.DistrictName));
            var selectModels = traineeBioDataGeneralInfos.Select(x => new SelectedModel
            {
                Text = x.DistrictName + " - " + x.DistrictNameBangla,
                Value = x.DistrictId
            }).ToList();
            return selectModels;
        }
    }
}
