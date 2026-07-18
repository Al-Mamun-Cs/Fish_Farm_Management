using MediatR;
using SchoolManagement.Application.DTOs.Districts;

namespace SchoolManagement.Application.Features.Districts.Requests.Queries
{
    public class GetDistrictDetailRequest : IRequest<DistrictDto>
    {
        public int DistrictId { get; set; }
    }
}
