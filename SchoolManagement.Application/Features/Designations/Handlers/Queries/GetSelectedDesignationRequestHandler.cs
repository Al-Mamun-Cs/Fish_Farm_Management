using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Designations.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Designations.Handlers.Queries
{
    public class GetSelectedDesignationRequestHandler : IRequestHandler<GetSelectedDesignationRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Designation> _DesignationRepository;


        public GetSelectedDesignationRequestHandler(ISchoolManagementRepository<Designation> DesignationRepository)
        {
            _DesignationRepository = DesignationRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDesignationRequest request, CancellationToken cancellationToken)
        {
            ICollection<Designation> codeValues = await _DesignationRepository.FilterAsync(x => x.WarehouseId == request.WarehouseId);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.DesignationId
            }).ToList();
            return selectModels;
        }
    }
}
