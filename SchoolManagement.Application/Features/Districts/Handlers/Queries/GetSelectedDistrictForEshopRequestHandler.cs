using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Districts.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Districts.Handlers.Queries
{
    public class GetSelectedDistrictForEshopRequestHandler : IRequestHandler<GetSelectedDistrictForEshopRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<District> _DistrictRepository;


        public GetSelectedDistrictForEshopRequestHandler(ISchoolManagementRepository<District> DistrictRepository)
        {
            _DistrictRepository = DistrictRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDistrictForEshopRequest request, CancellationToken cancellationToken)
        {
            ICollection<District> codeValues = await _DistrictRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.DistrictName,
                Value = x.DistrictId
            }).ToList();
            return selectModels;
        }
    }
}
