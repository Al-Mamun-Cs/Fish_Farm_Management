using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ProjectTypes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ProjectTypes.Handlers.Queries
{
    public class GetSelectedProjectTypeRequestHandler : IRequestHandler<GetSelectedProjectTypeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ProjectType> _ProjectTypeRepository;


        public GetSelectedProjectTypeRequestHandler(ISchoolManagementRepository<ProjectType> ProjectTypeRepository)
        {
            _ProjectTypeRepository = ProjectTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedProjectTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<ProjectType> codeValues = await _ProjectTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.NameBangla,
                Value = x.ProjectTypeId
            }).ToList();
            return selectModels;
        }
    }
}
