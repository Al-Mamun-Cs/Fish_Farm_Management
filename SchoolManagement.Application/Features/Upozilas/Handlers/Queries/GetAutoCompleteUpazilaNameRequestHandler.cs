using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Upozilas.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Upozilas.Handlers.Queries
{
    public class GetAutoCompleteUpazilaNameRequestHandler : IRequestHandler<GetAutoCompleteUpazilaNameRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Upozila> _UpozilaRepository;
        public GetAutoCompleteUpazilaNameRequestHandler(ISchoolManagementRepository<Upozila> UpozilaRepository)
        {
            _UpozilaRepository = UpozilaRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetAutoCompleteUpazilaNameRequest request, CancellationToken cancellationToken)
        {
            ICollection<Upozila> traineeBioDataGeneralInfos = await _UpozilaRepository.FilterAsync(x => x.UpazilaName.Contains(request.UpazilaName));
            var selectModels = traineeBioDataGeneralInfos.Select(x => new SelectedModel
            {
                Text = x.UpazilaName + " - " + x.UpazilaNameBangla,
                Value = x.UpazilaId
            }).ToList();
            return selectModels;
        }
    }
}
