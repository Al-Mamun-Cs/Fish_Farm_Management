using MediatR;
using SchoolManagement.Application.DTOs.Depositors;

namespace SchoolManagement.Application.Features.Depositors.Requests.Queries
{
    public class GetDepositorDetailRequest : IRequest<DepositorDto>
    {
        public int DepositorId { get; set; }
    }
}
