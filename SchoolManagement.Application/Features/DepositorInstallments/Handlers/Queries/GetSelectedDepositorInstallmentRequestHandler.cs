using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.DepositorInstallments.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.DepositorInstallments.Handlers.Queries
{
    public class GetSelectedDepositorInstallmentRequestHandler : IRequestHandler<GetSelectedDepositorInstallmentRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<DepositorInstallment> _DepositorInstallmentRepository;


        public GetSelectedDepositorInstallmentRequestHandler(ISchoolManagementRepository<DepositorInstallment> DepositorInstallmentRepository)
        {
            _DepositorInstallmentRepository = DepositorInstallmentRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDepositorInstallmentRequest request, CancellationToken cancellationToken)
        {
            ICollection<DepositorInstallment> codeValues = await _DepositorInstallmentRepository.FilterAsync(x => x.WarehouseId == request.WarehouseId);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.InstallmentAmount,
                Value = x.DepositorInstallmentId
            }).ToList();
            return selectModels;
        }
    }
}
