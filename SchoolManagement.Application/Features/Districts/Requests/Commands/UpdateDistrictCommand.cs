using MediatR;
using SchoolManagement.Application.DTOs.Districts;

namespace SchoolManagement.Application.Features.Districts.Requests.Commands
{
    public class UpdateDistrictCommand : IRequest<Unit>
    {
        public DistrictDto DistrictDto { get; set; }
    }
}
