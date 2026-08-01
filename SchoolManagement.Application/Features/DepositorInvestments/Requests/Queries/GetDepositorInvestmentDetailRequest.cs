using MediatR;
using SchoolManagement.Application.DTOs.DepositorInvestments;

namespace SchoolManagement.Application.Features.DepositorInvestments.Requests.Queries
{
    public class GetDepositorInvestmentDetailRequest : IRequest<DepositorInvestmentDto>
    {
        public int DepositorInvestmentId { get; set; }
    }
}
