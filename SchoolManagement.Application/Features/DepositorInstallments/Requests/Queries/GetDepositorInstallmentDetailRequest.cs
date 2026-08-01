using MediatR;
using SchoolManagement.Application.DTOs.DepositorInstallments;

namespace SchoolManagement.Application.Features.DepositorInstallments.Requests.Queries
{
    public class GetDepositorInstallmentDetailRequest : IRequest<DepositorInstallmentDto>
    {
        public int DepositorInstallmentId { get; set; }
    }
}
