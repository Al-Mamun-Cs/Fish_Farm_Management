using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.BloodGroups.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.BloodGroups.Handlers.Queries
{
    public class GetSelectedBloodGroupRequestHandler : IRequestHandler<GetSelectedBloodGroupRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<BloodGroup> _BloodGroupRepository;


        public GetSelectedBloodGroupRequestHandler(ISchoolManagementRepository<BloodGroup> BloodGroupRepository)
        {
            _BloodGroupRepository = BloodGroupRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedBloodGroupRequest request, CancellationToken cancellationToken)
        {
            ICollection<BloodGroup> codeValues = await _BloodGroupRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.FullName,
                Value = x.BloodGroupId
            }).ToList();
            return selectModels;
        }
    }
}
