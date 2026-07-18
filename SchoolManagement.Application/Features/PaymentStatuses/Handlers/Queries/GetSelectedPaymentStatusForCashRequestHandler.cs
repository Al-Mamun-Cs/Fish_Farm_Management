using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.PaymentStatuses.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PaymentStatuses.Handlers.Queries
{
    public class GetSelectedPaymentStatusForCashRequestHandler : IRequestHandler<GetSelectedPaymentStatusForCashRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<PaymentStatus> _PaymentStatusRepository;


        public GetSelectedPaymentStatusForCashRequestHandler(ISchoolManagementRepository<PaymentStatus> PaymentStatusRepository)
        {
            _PaymentStatusRepository = PaymentStatusRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedPaymentStatusForCashRequest request, CancellationToken cancellationToken)
        {
            ICollection<PaymentStatus> codeValues = await _PaymentStatusRepository.FilterAsync(x => x.PriorityNo == 2);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.StatusName,
                Value = x.PaymentStatusId
            }).ToList();
            return selectModels;
        }
    }
}
