using MediatR;
using SchoolManagement.Application.DTOs.Ponds;

namespace SchoolManagement.Application.Features.Ponds.Requests.Queries
{
    public class GetPondDetailRequest : IRequest<PondDto>
    {
        public int PondId { get; set; }
    }
}
