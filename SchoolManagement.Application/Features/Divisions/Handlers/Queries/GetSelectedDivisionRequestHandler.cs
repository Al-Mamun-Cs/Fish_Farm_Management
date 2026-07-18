using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Divisions.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Divisions.Handlers.Queries
{
    public class GetSelectedDivisionRequestHandler : IRequestHandler<GetSelectedDivisionRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Division> _DivisionRepository;


        public GetSelectedDivisionRequestHandler(ISchoolManagementRepository<Division> DivisionRepository)
        {
            _DivisionRepository = DivisionRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDivisionRequest request, CancellationToken cancellationToken)
        {
            ICollection<Division> codeValues = await _DivisionRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.DivisionName,
                Value = x.DivisionId
            }).ToList();
            return selectModels;
        }
    }
}
