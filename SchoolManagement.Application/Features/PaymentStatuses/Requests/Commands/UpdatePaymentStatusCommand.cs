using MediatR;
using SchoolManagement.Application.DTOs.PaymentStatuses;

namespace SchoolManagement.Application.Features.PaymentStatuses.Requests.Commands
{
    public class UpdatePaymentStatusCommand : IRequest<Unit>
    {
        public PaymentStatusDto PaymentStatusDto { get; set; }
    }
}
