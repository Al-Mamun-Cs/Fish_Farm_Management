using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PaymentStatuses.Requests.Queries
{
    public class GetSelectedPaymentStatusForCashRequest : IRequest<List<SelectedModel>>
    {
    }
}
