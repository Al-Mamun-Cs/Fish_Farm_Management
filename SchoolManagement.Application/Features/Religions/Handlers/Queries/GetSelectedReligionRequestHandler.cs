using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Religions.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Religions.Handlers.Queries
{
    public class GetSelectedReligionRequestHandler : IRequestHandler<GetSelectedReligionRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Religion> _ReligionRepository;


        public GetSelectedReligionRequestHandler(ISchoolManagementRepository<Religion> ReligionRepository)
        {
            _ReligionRepository = ReligionRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedReligionRequest request, CancellationToken cancellationToken)
        {
            ICollection<Religion> codeValues = await _ReligionRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.FullName,
                Value = x.ReligionId
            }).ToList();
            return selectModels;
        }
    }
}
