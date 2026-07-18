using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Designations.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Designations.Handlers.Queries
{
    public class GetSelectedDesignationForManagementRequestHandler : IRequestHandler<GetSelectedDesignationForManagementRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Designation> _DesignationRepository;


        public GetSelectedDesignationForManagementRequestHandler(ISchoolManagementRepository<Designation> DesignationRepository)
        {
            _DesignationRepository = DesignationRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDesignationForManagementRequest request, CancellationToken cancellationToken)
        {
            ICollection<Designation> codeValues = await _DesignationRepository.FilterAsync(x => x.WarehouseId == null);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.DesignationId
            }).ToList();
            return selectModels;
        }
    }
}
