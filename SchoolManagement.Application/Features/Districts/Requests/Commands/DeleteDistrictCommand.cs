using MediatR;

namespace SchoolManagement.Application.Features.Districts.Requests.Commands
{
    public class DeleteDistrictCommand : IRequest
    {
        public int DistrictId { get; set; }
    }
}
