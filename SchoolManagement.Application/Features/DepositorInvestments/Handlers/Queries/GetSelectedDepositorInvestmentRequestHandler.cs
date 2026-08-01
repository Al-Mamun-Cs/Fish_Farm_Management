using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.DepositorInvestments.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.DepositorInvestments.Handlers.Queries
{
    public class GetSelectedDepositorInvestmentRequestHandler : IRequestHandler<GetSelectedDepositorInvestmentRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<DepositorInvestment> _DepositorInvestmentRepository;


        public GetSelectedDepositorInvestmentRequestHandler(ISchoolManagementRepository<DepositorInvestment> DepositorInvestmentRepository)
        {
            _DepositorInvestmentRepository = DepositorInvestmentRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDepositorInvestmentRequest request, CancellationToken cancellationToken)
        {
            ICollection<DepositorInvestment> codeValues = await _DepositorInvestmentRepository.FilterAsync(x => x.WarehouseId == request.WarehouseId);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.BusinessOperatorName,
                Value = x.DepositorInvestmentId
            }).ToList();
            return selectModels;
        }
    }
}
