using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Depositors.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Depositors.Handlers.Queries
{
    public class GetSelectedDepositorRequestHandler : IRequestHandler<GetSelectedDepositorRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Depositor> _DepositorRepository;


        public GetSelectedDepositorRequestHandler(ISchoolManagementRepository<Depositor> DepositorRepository)
        {
            _DepositorRepository = DepositorRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDepositorRequest request, CancellationToken cancellationToken)
        {
            ICollection<Depositor> codeValues = await _DepositorRepository.FilterAsync(x => x.WarehouseId == request.WarehouseId);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.DepositorName,
                Value = x.DepositorId
            }).ToList();
            return selectModels;
        }
    }
}
