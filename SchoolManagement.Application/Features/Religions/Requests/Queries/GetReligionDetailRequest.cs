using MediatR;
using SchoolManagement.Application.DTOs.Religions;

namespace SchoolManagement.Application.Features.Religions.Requests.Queries
{
    public class GetReligionDetailRequest : IRequest<ReligionDto>
    {
        public int ReligionId { get; set; }
    }
}
