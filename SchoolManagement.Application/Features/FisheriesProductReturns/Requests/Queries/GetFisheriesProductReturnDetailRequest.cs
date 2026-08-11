using MediatR;
using SchoolManagement.Application.DTOs.FisheriesProductReturns;

namespace SchoolManagement.Application.Features.FisheriesProductReturns.Requests.Queries
{
    public class GetFisheriesProductReturnDetailRequest : IRequest<FisheriesProductReturnDto>
    {
        public int FisheriesProductReturnId { get; set; }
    }
}
